using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip fuseSound; // 導火線のサウンド
    public AudioClip explosionSound; // 爆発のサウンド
    private AudioSource audioSource;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでAudioManagerを維持する
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayFuseSound();

        if (audioSource == null)
        {
            Debug.LogError("AudioSourceコンポーネントがAudioManagerにアタッチされていません。");
        }
    }

    public void PlayFuseSound()
    {
        if (audioSource && fuseSound)
        {
            audioSource.PlayOneShot(fuseSound);
        }
    }

    public void PlayExplosionSound()
    {
        if (audioSource && explosionSound)
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }
}
