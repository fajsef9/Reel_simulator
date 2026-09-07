using UnityEngine;
using System.Collections;

public class HomeLogoStamp : MonoBehaviour
{
    [SerializeField] private float startScale = 2.5f;
    [SerializeField] private float slamTime = 0.18f;
    [SerializeField] private float overshootScale = 1.1f;
    [SerializeField] private float settleTime = 0.12f;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip stampSound;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        transform.localScale = originalScale * startScale;

        StartCoroutine(SlamIn());
    }

    private IEnumerator SlamIn()
    {
        float timer = 0f;

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

            yield return null;
        }

        if (audioSource != null && stampSound != null)
        {
            audioSource.PlayOneShot(stampSound);
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
}