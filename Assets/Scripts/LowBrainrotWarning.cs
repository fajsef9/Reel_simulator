using UnityEngine;
using System.Collections;

public class LowBrainrotWarning : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip warningSound;

    [SerializeField] private float warningThreshold = 0.25f;
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private float pulseSpeed = 3f;
    [SerializeField] private float minAlpha = 0.15f;
    [SerializeField] private float maxAlpha = 0.65f;

    private bool isLow = false;
    private bool warningPlayed = false;

    private void Start()
    {
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        if (isLow)
        {
            float pulse =
                Mathf.PingPong(
                    Time.time * pulseSpeed,
                    1f
                );

            float targetAlpha = Mathf.Lerp(
                minAlpha,
                maxAlpha,
                pulse
            );

            canvasGroup.alpha = Mathf.Lerp(
                canvasGroup.alpha,
                targetAlpha,
                Time.deltaTime * fadeSpeed
            );
        }
        else
        {
            canvasGroup.alpha = Mathf.Lerp(
                canvasGroup.alpha,
                0f,
                Time.deltaTime * fadeSpeed
            );
        }
    }

    public void UpdateBrainrot(float normalizedBrainrot)
    {
        if (normalizedBrainrot <= warningThreshold)
        {
            if (!isLow)
            {
                isLow = true;

                if (!warningPlayed && warningSound != null)
                {
                    audioSource.PlayOneShot(warningSound);
                    warningPlayed = true;
                }
            }
        }
        else
        {
            isLow = false;
            warningPlayed = false;
        }
    }
}