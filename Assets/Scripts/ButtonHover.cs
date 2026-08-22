using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverScale = 1.07f;
    [SerializeField] private float hoverRotation = 5f;
    [SerializeField] private float speed = 12f;

    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Vector3 targetScale;
    private Quaternion targetRotation;

    private void Awake()
    {
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
        targetScale = originalScale;
        targetRotation = originalRotation;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            speed * Time.unscaledDeltaTime
        );

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            speed * Time.unscaledDeltaTime
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;

        targetRotation = originalRotation *
            Quaternion.Euler(0f, 0f, hoverRotation);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        targetRotation = originalRotation;
    }
}