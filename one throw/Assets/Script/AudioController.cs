using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip fuseSound;       // SE1: プレハブが存在する間ループで流れるサウンド
    public AudioClip explosionSound;  // SE2: プレハブがインスタンスされたときに1回流れるサウンド

    private AudioSource audioSource;

    private void Start()
    {
        // AudioSourceのチェックと追加
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceを追加
        }

        // SE1: fuseSoundをループ再生
        if (fuseSound != null)
        {
            audioSource.clip = fuseSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        // SE2: プレハブがインスタンスされたときに1回流す
        if (explosionSound != null)
        {
            AudioManager.Instance.PlayExplosionSound(explosionSound);
        }
    }
}
