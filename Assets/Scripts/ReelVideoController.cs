using UnityEngine;
using UnityEngine.Video;

public class ReelVideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }
}