using UnityEngine;
using System.Collections.Generic;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; }  // シングルトンインスタンス

    [Header("Audio Clips")]
    public AudioClip bgmClip;  // BGMのAudio Clip

    public List<AudioClip> seClips;   // 複数のSEのAudio Clipリスト

    private AudioSource bgmAudioSource;  // BGM用のAudio Source
    private List<AudioSource> seAudioSources;   // 複数のSE用のAudio Sourceリスト

    // BGMとSEのボリュームのプロパティ
    public float BGMVolume
    {
        get => bgmAudioSource != null ? bgmAudioSource.volume : 0f;
        set
        {
            if (bgmAudioSource != null)
                bgmAudioSource.volume = value;
        }
    }

    public float SEVolume
    {
        get
        {
            if (seAudioSources.Count > 0 && seAudioSources[0] != null)
                return seAudioSources[0].volume;
            else
                return 0f;
        }
        set
        {
            foreach (AudioSource source in seAudioSources)
            {
                if (source != null)
                    source.volume = value;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間でオブジェクトを保持する

            // BGM用のAudio Sourceを設定
            bgmAudioSource = gameObject.AddComponent<AudioSource>();
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.loop = true; // BGMをループする

            // 複数のSE用のAudio Sourceを設定
            seAudioSources = new List<AudioSource>();
            foreach (AudioClip clip in seClips)
            {
                AudioSource seSource = gameObject.AddComponent<AudioSource>();
                seSource.clip = clip;
                seAudioSources.Add(seSource);
            }
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は破棄する
        }
    }

    // BGMの再生を開始するメソッド
    public void PlayBGM()
    {
        if (bgmAudioSource != null && bgmClip != null)
        {
            bgmAudioSource.Play();
        }
    }

    // 特定のSEの再生をするメソッド
    public void PlaySE(int index)
    {
        if (index >= 0 && index < seAudioSources.Count)
        {
            AudioSource seSource = seAudioSources[index];
            if (seSource != null)
            {
                seSource.PlayOneShot(seSource.clip);
            }
        }
    }

    // SEの全リストから任意のSEを取得して再生する
    public void PlaySEByName(string seName)
    {
        foreach (AudioSource seSource in seAudioSources)
        {
            if (seSource.clip != null && seSource.clip.name == seName)
            {
                seSource.PlayOneShot(seSource.clip);
                return;
            }
        }

        Debug.LogWarning("SE " + seName + " not found.");
    }
}
