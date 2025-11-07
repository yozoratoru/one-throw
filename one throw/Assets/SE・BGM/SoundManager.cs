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
            //Debug.Log($"[SoundManager] Awake: seClips={seClips.Count} seClipDict={seClipDict.Count} bgmAudioSource={(bgmAudioSource!=null)} seAudioSourcePrefab={(seAudioSourcePrefab!=null)}");

            // ビルド時にインスペクタの参照が切れている可能性があるため、null の場合は動的に作成してフォールバック
            if (bgmAudioSource == null)
            {
                var go = new GameObject("BGM_AudioSource",
                    typeof(AudioSource));
                go.transform.SetParent(transform);
                bgmAudioSource = go.GetComponent<AudioSource>();
                bgmAudioSource.playOnAwake = false;
                bgmAudioSource.loop = true;
                bgmAudioSource.spatialBlend = 0f;
                //Debug.Log("[SoundManager] Awake: bgmAudioSource was null, created fallback AudioSource.");
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
        //Debug.Log("[SoundManager] currentSEVolume: new=" + currentSEVolume);

        // 現在保持している再生中のAudioSourceに対して音量更新（null要素を取り除く）
        for (int i = seAudioSources.Count - 1; i >= 0; i--)
        {
            var se = seAudioSources[i];
            if (se == null)
            {
                seAudioSources.RemoveAt(i);
                continue;
            }
            se.volume = currentSEVolume;
        }

        if (loopSEAudioSource != null)
        {
            loopSEAudioSource.volume = currentSEVolume;
        }

        //Debug.Log($"[SoundManager] SetSEVolume: input={volume} currentSEVolume={currentSEVolume} activeSECount={seAudioSources.Count}");
    }

    /// <summary>
    /// SEを名前で単発再生
    /// </summary>
    public void PlaySE(string seName)
    {
        if (seClipDict.TryGetValue(seName, out AudioClip clip))
        {
            AudioSource se = null;
            if (seAudioSourcePrefab != null)
            {
                se = Instantiate(seAudioSourcePrefab, transform);
            }
            else
            {
                // フォールバック: プレハブが割り当てられていない（ビルドで参照切れなど）の場合は動的生成
                var go = new GameObject($"SE_AudioSource_{seName}");
                go.transform.SetParent(transform);
                se = go.AddComponent<AudioSource>();
            }

            se.clip = clip;
            // 2Dにして距離減衰の影響を受けないようにする（必要ならInspectorで変更してください）
            se.spatialBlend = 0f;
            se.playOnAwake = false;
            se.volume = currentSEVolume;
            se.Play();
            seAudioSources.Add(se);
            //Debug.Log($"[SoundManager] PlaySE: '{seName}' clipLength={(clip!=null?clip.length:0f)} currentSEVolume={currentSEVolume} createdVolume={se.volume} (usedPrefab={(seAudioSourcePrefab!=null)})");
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

            Debug.Log($"[SoundManager] currentSEVolume={currentSEVolume}");
        if (seClipDict.TryGetValue(seName, out AudioClip clip))
        {
            if (seAudioSourcePrefab != null)
            {
                loopSEAudioSource = Instantiate(seAudioSourcePrefab, transform);
            }
            else
            {
                var go = new GameObject($"LoopSE_AudioSource_{seName}");
                go.transform.SetParent(transform);
                loopSEAudioSource = go.AddComponent<AudioSource>();
            }

            loopSEAudioSource.clip = clip;
            loopSEAudioSource.spatialBlend = 0f;
            loopSEAudioSource.playOnAwake = false;
            loopSEAudioSource.volume = currentSEVolume;
            loopSEAudioSource.loop = true;
            loopSEAudioSource.Play();
            Debug.Log($"[SoundManager] PlayLoopingSE: '{seName}' currentSEVolume={currentSEVolume} (usedPrefab={(seAudioSourcePrefab!=null)})");
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
        float wait = 0f;
        if (source != null && source.clip != null)
        {
            wait = source.clip.length;
        }
        yield return new WaitForSeconds(wait);
        if (seAudioSources.Contains(source)) seAudioSources.Remove(source);
        if (source != null) Destroy(source.gameObject);
    }
}
