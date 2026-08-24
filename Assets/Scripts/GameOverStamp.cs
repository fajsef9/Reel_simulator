using UnityEngine;
using System.Collections;

public class GameOverStamp : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float startScale = 2f;
    [SerializeField] private float stampDuration = 0.12f;
    [SerializeField] private float bounceScale = 0.95f;
    [SerializeField] private float bounceDuration = 0.08f;

    [Header("Sound")]
    [SerializeField] private AudioClip stampSound;
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;
    private Vector3 finalScale;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        finalScale = transform.localScale;
    }

    public void Stamp()
    {
        StopAllCoroutines();
        StartCoroutine(StampRoutine());
    }

    private IEnumerator StampRoutine()
    {
        transform.localScale = finalScale * startScale;

        audioSource.PlayOneShot(
            stampSound,
            volume
        );

        float timer = 0f;

        while (timer < stampDuration)
        {
            timer += Time.unscaledDeltaTime;

            transform.localScale = Vector3.Lerp(
                finalScale * startScale,
                finalScale * bounceScale,
                timer / stampDuration
            );

            yield return null;
        }

        timer = 0f;

        while (timer < bounceDuration)
        {
            timer += Time.unscaledDeltaTime;

            transform.localScale = Vector3.Lerp(
                finalScale * bounceScale,
                finalScale,
                timer / bounceDuration
            );

            yield return null;
        }

        transform.localScale = finalScale;
    }
}