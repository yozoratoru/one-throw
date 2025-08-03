using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM設定")]
    [SerializeField] private AudioSource bgmAudioSource;
    [Range(0f, 1f)] [SerializeField] private float maxVolume = 0.5f;

    [Header("SE設定")]
    [SerializeField] private AudioSource seAudioSourcePrefab;
    [Range(0f, 1f)] [SerializeField] private float maxSEVolume = 1.0f;

    [SerializeField] private List<AudioClip> seClips = new List<AudioClip>();
    private Dictionary<string, AudioClip> seClipDict = new Dictionary<string, AudioClip>();

    private List<AudioSource> seAudioSources = new List<AudioSource>();
    private AudioSource loopSEAudioSource = null;
    private float currentSEVolume = 1.0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioClip名をキーとして辞書に登録
            foreach (var clip in seClips)
            {
                if (clip != null && !seClipDict.ContainsKey(clip.name))
                {
                    seClipDict.Add(clip.name, clip);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// BGMの音量を設定（0〜1）
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        bgmAudioSource.volume = Mathf.Clamp01(volume) * maxVolume;
    }

    /// <summary>
    /// SEの音量を設定（0〜1）
    /// </summary>
    public void SetSEVolume(float volume)
    {
        currentSEVolume = Mathf.Clamp01(volume) * maxSEVolume;

        foreach (var se in seAudioSources)
        {
            if (se != null)
            {
                se.volume = currentSEVolume;
            }
        }

        if (loopSEAudioSource != null)
        {
            loopSEAudioSource.volume = currentSEVolume;
        }
    }

    /// <summary>
    /// SEを名前で単発再生
    /// </summary>
    public void PlaySE(string seName)
    {
        if (seClipDict.TryGetValue(seName, out AudioClip clip))
        {
            AudioSource se = Instantiate(seAudioSourcePrefab, transform);
            se.clip = clip;
            se.volume = currentSEVolume;
            se.Play();
            seAudioSources.Add(se);
            StartCoroutine(DestroyAfterPlay(se));
        }
        else
        {
            Debug.LogWarning($"SE '{seName}' が見つかりませんでした。");
        }
    }

    /// <summary>
    /// SEをループで再生（すでに再生中でなければ）
    /// </summary>
    public void PlayLoopingSE(string seName)
    {
        if (loopSEAudioSource != null) return;

        if (seClipDict.TryGetValue(seName, out AudioClip clip))
        {
            loopSEAudioSource = Instantiate(seAudioSourcePrefab, transform);
            loopSEAudioSource.clip = clip;
            loopSEAudioSource.volume = currentSEVolume;
            loopSEAudioSource.loop = true;
            loopSEAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"ループSE '{seName}' が見つかりませんでした。");
        }
    }

    /// <summary>
    /// ループSEを停止
    /// </summary>
    public void StopLoopingSE()
    {
        if (loopSEAudioSource != null)
        {
            loopSEAudioSource.Stop();
            Destroy(loopSEAudioSource.gameObject);
            loopSEAudioSource = null;
        }
    }

    /// <summary>
    /// SE再生後にAudioSourceを破棄
    /// </summary>
    private IEnumerator DestroyAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        seAudioSources.Remove(source);
        Destroy(source.gameObject);
    }
}
