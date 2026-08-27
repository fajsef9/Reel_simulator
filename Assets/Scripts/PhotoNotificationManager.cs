using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhoneNotificationManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image notificationImage;
    [SerializeField] private CanvasGroup notificationGroup;
    [SerializeField] private RectTransform notificationTransform;

    [Header("Notification Images")]
    [SerializeField] private Sprite[] notificationSprites;

    [Header("Timing")]
    [SerializeField] private float minTimeBetweenNotifications = 8f;
    [SerializeField] private float maxTimeBetweenNotifications = 20f;
    [SerializeField] private float displayTime = 3f;

    [Header("Animation")]
    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float slideDistance = 50f;
    [SerializeField] private float slideSpeed = 8f;

    [Header("Game")]
    [SerializeField] private PhoneController phoneController;
    [SerializeField] private GameManager gameManager;
    [Header("Audio")]
    [SerializeField] private AudioSource notificationAudioSource;
    [SerializeField] private AudioClip notificationSound;

    private bool isShowing;
    private Vector2 originalPosition;

    private void Start()
    {
        notificationGroup.alpha = 0f;
        originalPosition = notificationTransform.anchoredPosition;

        StartCoroutine(NotificationRoutine());
    }

    private IEnumerator NotificationRoutine()
    {
        while (!gameManager.IsGameOver)
        {
            float waitTime = Random.Range(
                minTimeBetweenNotifications,
                maxTimeBetweenNotifications
            );

            yield return new WaitForSeconds(waitTime);

            if (
                !gameManager.IsGameOver &&
                phoneController.IsPhoneOut &&
                !isShowing
            )
            {
                StartCoroutine(ShowRandomNotification());
            }
        }
    }

    private IEnumerator ShowRandomNotification()
    {
        if (
            notificationSprites == null ||
            notificationSprites.Length == 0
        )
            yield break;

        isShowing = true;

        notificationImage.sprite =
            notificationSprites[
                Random.Range(0, notificationSprites.Length)
            ];

        if (
            notificationAudioSource != null &&
            notificationSound != null
        )
        {
            notificationAudioSource.PlayOneShot(notificationSound);
        }
        
        notificationTransform.anchoredPosition =
            originalPosition + Vector2.up * slideDistance;

        notificationGroup.alpha = 0f;

        float animationProgress = 0f;

        while (animationProgress < 1f)
        {
            animationProgress +=
                Time.deltaTime * slideSpeed;

            notificationGroup.alpha =
                Mathf.Lerp(
                    0f,
                    1f,
                    animationProgress
                );

            notificationTransform.anchoredPosition =
                Vector2.Lerp(
                    originalPosition + Vector2.up * slideDistance,
                    originalPosition,
                    animationProgress
                );

            yield return null;
        }

        notificationGroup.alpha = 1f;
        notificationTransform.anchoredPosition = originalPosition;

        yield return new WaitForSeconds(displayTime);

        animationProgress = 0f;

        while (animationProgress < 1f)
        {
            animationProgress +=
                Time.deltaTime * fadeSpeed;

            notificationGroup.alpha =
                Mathf.Lerp(
                    1f,
                    0f,
                    animationProgress
                );

            yield return null;
        }

        notificationGroup.alpha = 0f;

        isShowing = false;
    }
}