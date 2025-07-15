using UnityEngine;
using UnityEngine.Video;

public class VideoLoop : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }
}
