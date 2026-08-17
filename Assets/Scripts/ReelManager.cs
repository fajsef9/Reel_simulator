using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class ReelManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip[] reelPool;

    [SerializeField] private RectTransform content;
    [SerializeField] private float snapSpeed = 10f;

    private int currentReel = 0;
    private float reelHeight;

    private void Start()
    {
        reelHeight = content.GetChild(0)
            .GetComponent<RectTransform>()
            .rect.height;

        PlayRandomReel();
    }

    private void Update()
    {
        // float scroll = Mouse.current.scroll.ReadValue().y;

        // if (scroll < 0)
        // {
        //     GoToNextReel();
        // }

        // float targetY = currentReel * reelHeight;

        // Vector2 targetPosition = new Vector2(
        //     content.anchoredPosition.x,
        //     targetY
        // );

        // content.anchoredPosition = Vector2.Lerp(
        //     content.anchoredPosition,
        //     targetPosition,
        //     snapSpeed * Time.deltaTime
        // );
    }

    private void GoToNextReel()
    {
        if (currentReel >= content.childCount - 1)
            return;

        currentReel++;

        PlayRandomReel();
    }

    private void PlayRandomReel()
    {
        if (reelPool.Length == 0)
        {
            Debug.LogWarning("Reel pool is empty!");
            return;
        }

        int randomIndex = Random.Range(0, reelPool.Length);

        videoPlayer.Stop();

        videoPlayer.clip = reelPool[randomIndex];

        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.prepareCompleted += OnVideoPrepared;

        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        vp.prepareCompleted -= OnVideoPrepared;

        vp.Play();
    }
}