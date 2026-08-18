using UnityEngine;
using TMPro;
using System.Collections;

public class RarityPopupAnimator : MonoBehaviour
{
    [SerializeField] private TMP_Text rarityText;

    [Header("Animation")]
    [SerializeField] private float animationDuration = 1.2f;
    [SerializeField] private float moveDistance = 100f;

    private Vector2 startPosition;
    private Coroutine currentAnimation;

    private void Awake()
    {
        startPosition = rarityText.rectTransform.anchoredPosition;

        // Start invisible instead of disabling the GameObject
        Color color = rarityText.color;
        color.a = 0f;
        rarityText.color = color;
    }

    public void Show(string text, Color color)
    {
        rarityText.text = text;

        color.a = 1f;
        rarityText.color = color;

        rarityText.rectTransform.anchoredPosition = startPosition;

        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        currentAnimation = StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        Color startColor = rarityText.color;

        Vector2 endPosition =
            startPosition + Vector2.up * moveDistance;

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.unscaledDeltaTime;

            float progress =
                Mathf.Clamp01(timer / animationDuration);

            // Move upward
            rarityText.rectTransform.anchoredPosition =
                Vector2.Lerp(
                    startPosition,
                    endPosition,
                    progress
                );

            // Fade out
            Color color = startColor;

            color.a = Mathf.Lerp(
                1f,
                0f,
                progress
            );

            rarityText.color = color;

            yield return null;
        }

        // Make sure it ends completely invisible
        Color finalColor = rarityText.color;
        finalColor.a = 0f;
        rarityText.color = finalColor;
    }
}