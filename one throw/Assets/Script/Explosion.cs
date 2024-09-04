using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip explosionSound; // SE2: プレハブがインスタンスされたときに1回流すサウンド

    private AudioSource explosionAudioSource;

    private void Start()
    {
        // 既存のAudioSourceコンポーネントを取得
        explosionAudioSource = GetComponent<AudioSource>();

        if (explosionAudioSource != null && explosionSound != null)
        {
            explosionAudioSource.clip = explosionSound;
            explosionAudioSource.loop = false; // ループを無効にする

            // AudioManager に explosionAudioSource を設定
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetExplosionAudioSource(explosionAudioSource);
            }

            explosionAudioSource.Play(); // explosionSoundを再生
        }
        else
        {
            Debug.LogWarning("AudioSource or explosionSound is missing.");
        }
    }
}
