using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip fuseSound;       // Sound effect for the fuse
    public AudioClip explosionSound;  // Sound effect for the explosion

    private AudioSource audioSource;

    private void Start()
    {
        // Check and add AudioSource if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource
        }

        // Play the fuse sound effect in a loop while the object exists
        if (fuseSound != null)
        {
            audioSource.clip = fuseSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayExplosionSound()
    {
        if (explosionSound != null)
        {
            // Use AudioManager to play the explosion sound effect once
            AudioManager.Instance.PlaySFX(explosionSound);
        }
    }

    // This function could be called to stop the fuse sound if needed
    public void StopFuseSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}