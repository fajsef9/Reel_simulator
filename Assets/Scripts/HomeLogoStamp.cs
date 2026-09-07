using UnityEngine;
using System.Collections;

public class HomeLogoStamp : MonoBehaviour
{
    [Header("Stamp")]
    [SerializeField] private float startScale = 2.5f;
    [SerializeField] private float slamTime = 0.18f;
    [SerializeField] private float overshootScale = 1.1f;
    [SerializeField] private float settleTime = 0.12f;

    [Header("Stay")]
    [SerializeField] private float stayTime = 2f;

    [Header("Fall")]
    [SerializeField] private float fallDistance = 1000f;
    [SerializeField] private float fallTime = 2.5f;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip stampSound;

    private Vector3 originalScale;
    private Vector2 originalPosition;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        originalScale = transform.localScale;
        originalPosition = rectTransform.anchoredPosition;

        transform.localScale = originalScale * startScale;

        StartCoroutine(StampRoutine());
    }

    private IEnumerator StampRoutine()
    {
        yield return StartCoroutine(SlamIn());

        yield return new WaitForSeconds(stayTime);

        yield return StartCoroutine(FallAway());
    }

    private IEnumerator SlamIn()
    {
        float timer = 0f;
        bool soundPlayed = false;

        while (timer < slamTime)
        {
            timer += Time.deltaTime;

            float t = timer / slamTime;

            t = 1f - Mathf.Pow(1f - t, 3f);

            transform.localScale = Vector3.Lerp(
                originalScale * startScale,
                originalScale * overshootScale,
                t
            );

            if (!soundPlayed && timer >= slamTime * 0.8f)
            {
                soundPlayed = true;

                if (audioSource != null && stampSound != null)
                {
                    audioSource.PlayOneShot(stampSound);
                }
            }

            yield return null;
        }

        timer = 0f;

        while (timer < settleTime)
        {
            timer += Time.deltaTime;

            float t = timer / settleTime;

            t = Mathf.SmoothStep(0f, 1f, t);

            transform.localScale = Vector3.Lerp(
                originalScale * overshootScale,
                originalScale,
                t
            );

            yield return null;
        }

        transform.localScale = originalScale;
    }

    private IEnumerator FallAway()
    {
        Vector2 startPosition = rectTransform.anchoredPosition;

        Vector2 endPosition = startPosition +
                              Vector2.down * fallDistance;

        float timer = 0f;

        while (timer < fallTime)
        {
            timer += Time.deltaTime;

            float t = timer / fallTime;

            t = Mathf.SmoothStep(0f, 1f, t);

            rectTransform.anchoredPosition = Vector2.Lerp(
                startPosition,
                endPosition,
                t
            );

            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
    }
}