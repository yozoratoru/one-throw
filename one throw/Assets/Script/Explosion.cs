using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip explosionSound; // SE2: プレハブがインスタンスされたときに1回流すサウンド

    private AudioSource explosionAudioSource;

    private void Start()
    {
        if (explosionSound != null)
        {
            explosionAudioSource = gameObject.AddComponent<AudioSource>(); // Explosion用のAudioSourceを追加
            explosionAudioSource.clip = explosionSound;
            explosionAudioSource.loop = false; // ループを無効にする

            // AudioManager に explosionAudioSource を設定
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetExplosionAudioSource(explosionAudioSource);
            }

            explosionAudioSource.Play(); // explosionSoundを再生
        }
    }
}
