using UnityEngine;
using UnityEngine.InputSystem;

public class ReelSnapController : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private float snapSpeed = 10f;

    private int currentReel = 0;
    private float reelHeight;

    private void Start()
    {
        reelHeight = content.GetChild(0)
            .GetComponent<RectTransform>()
            .rect.height;
    }

    private void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll < 0)
        {
            currentReel = Mathf.Min(
                currentReel + 1,
                content.childCount - 1
            );
        }
        else if (scroll > 0)
        {
            currentReel = Mathf.Max(
                currentReel - 1,
                0
            );
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