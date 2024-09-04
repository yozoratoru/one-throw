using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  // Singleton instance

    public AudioSource bgmSource;    // BGM AudioSource
    public AudioSource sfxSource;    // SFX AudioSource
    public AudioSource explosionAudioSource; // Explosion AudioSource

    public AudioClip bgmClip;        // BGM clip

    public Slider bgmSlider;         // BGM Slider
    public Slider sfxSlider;         // SFX Slider

    private const string BGMVolumeKey = "BGMVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize AudioSources
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        PlayBGM();
        SetupSliders(); // Initialize sliders
    }

    private void OnEnable()
    {
        SetupSliders(); // Re-setup sliders on enable
    }

    public void PlayBGM()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;  // Loop BGM
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
            explosionAudioSource.loop = false; // Disable looping
            explosionAudioSource.volume = sfxSource.volume; // Set the volume to match SFX volume
            explosionAudioSource.Play(); // Play the explosion sound
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
            PlayerPrefs.SetFloat(BGMVolumeKey, volume); // Save volume
            PlayerPrefs.Save();
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;

            // Update explosionAudioSource volume if it exists
            if (explosionAudioSource != null)
            {
                explosionAudioSource.volume = volume;
            }

            PlayerPrefs.SetFloat(SFXVolumeKey, volume); // Save volume
            PlayerPrefs.Save();
        }
    }

    private void SetupSliders()
    {
        if (bgmSlider != null)
        {
            float savedBGMVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 1f); // Default to 1f
            bgmSlider.value = savedBGMVolume;
            bgmSlider.onValueChanged.RemoveAllListeners(); // Remove existing listeners
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f); // Default to 1f
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.RemoveAllListeners(); // Remove existing listeners
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetExplosionAudioSource(AudioSource source)
    {
        explosionAudioSource = source;
        explosionAudioSource.volume = sfxSource.volume; // Ensure the volume is consistent with SFX
    }
}
