using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  // シングルトンインスタンス

    public AudioSource bgmSource;    // BGM用のAudioSource
    public AudioSource sfxSource;    // 効果音用のAudioSource
    public AudioSource explosionAudioSource; // Explosion用のAudioSource

    public AudioClip bgmClip;        // BGMクリップ

    public Slider bgmSlider;         // BGM用スライダー
    public Slider sfxSlider;         // 効果音用スライダー

    private const string BGMVolumeKey = "BGMVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // AudioSourceの初期化
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        // explosionAudioSourceはプレハブインスタンス時に追加する
    }

    private void Start()
    {
        PlayBGM();
        SetupSliders(); // スライダーの初期設定
    }

    private void OnEnable()
    {
        // イベントリスナーの再設定
        SetupSliders();
    }

    public void PlayBGM()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;  // BGMをループで再生
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayExplosionSound(AudioClip clip)
    {
        if (explosionAudioSource != null && clip != null)
        {
            explosionAudioSource.clip = clip;
            explosionAudioSource.loop = false; // ループを無効にする
            explosionAudioSource.Play(); // explosionSoundを再生
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
            PlayerPrefs.SetFloat(BGMVolumeKey, volume); // 音量を保存
            PlayerPrefs.Save(); // 保存する
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat(SFXVolumeKey, volume); // 音量を保存
            PlayerPrefs.Save(); // 保存する
        }
    }

    private void SetupSliders()
    {
        if (bgmSlider != null)
        {
            float savedBGMVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 1f); // デフォルト値1f
            bgmSlider.value = savedBGMVolume;
            bgmSlider.onValueChanged.RemoveAllListeners(); // 既存のリスナーを削除
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f); // デフォルト値1f
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.RemoveAllListeners(); // 既存のリスナーを削除
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetExplosionAudioSource(AudioSource source)
    {
        explosionAudioSource = source;
    }
}
