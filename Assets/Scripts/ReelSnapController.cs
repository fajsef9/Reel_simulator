using UnityEngine;
using UnityEngine.InputSystem;

public class ReelSnapController : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private float snapSpeed = 10f;
    [SerializeField] private ReelVideoController[] reels;

    private int currentReel = 0;
    private float reelHeight;

    private void Start()
    {
        reelHeight = content.GetChild(0)
            .GetComponent<RectTransform>()
            .rect.height;

        reels[0].PlayVideo();
    }

    private void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll < 0)
        {
            int previousReel = currentReel;

            currentReel = Mathf.Min(
                currentReel + 1,
                content.childCount - 1
            );

            if (currentReel != previousReel)
            {
                reels[previousReel].StopVideo();
                reels[currentReel].PlayVideo();
            }
        }
        else if (scroll > 0)
        {
            int previousReel = currentReel;

            currentReel = Mathf.Max(
                currentReel - 1,
                0
            );

            if (currentReel != previousReel)
            {
                reels[previousReel].StopVideo();
                reels[currentReel].PlayVideo();
            }
        }

        float targetY = currentReel * reelHeight;

        Vector2 targetPosition = new Vector2(
            content.anchoredPosition.x,
            targetY
        );

        content.anchoredPosition = Vector2.Lerp(
            content.anchoredPosition,
            targetPosition,
            snapSpeed * Time.deltaTime
        );
    }
}