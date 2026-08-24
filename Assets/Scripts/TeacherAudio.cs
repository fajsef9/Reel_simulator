using UnityEngine;
using System.Collections;

public class TeacherAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource teachingAudioSource;
    [SerializeField] private AudioSource angryAudioSource;

    [Header("Teaching Audio")]
    [SerializeField] private AudioClip[] teachingSounds;
    [SerializeField] private float minDelay = 0.5f;
    [SerializeField] private float maxDelay = 2f;

    [Header("Angry Audio")]
    [SerializeField] private AudioClip angrySound;

    private bool teacherCaught = false;
    private Coroutine teachingRoutine;

    private void Start()
    {
        teachingRoutine = StartCoroutine(TeachingRoutine());
    }

    private IEnumerator TeachingRoutine()
    {
        while (!teacherCaught)
        {
            if (teachingSounds.Length > 0)
            {
                AudioClip randomSound = teachingSounds[
                    Random.Range(0, teachingSounds.Length)
                ];

                teachingAudioSource.clip = randomSound;
                teachingAudioSource.Play();

                yield return new WaitForSeconds(randomSound.length);

                if (teacherCaught)
                    yield break;

                float delay = Random.Range(
                    minDelay,
                    maxDelay
                );

                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void StopTeaching()
    {
        teacherCaught = true;

        if (teachingRoutine != null)
        {
            StopCoroutine(teachingRoutine);
        }

        teachingAudioSource.Stop();
    }

    public IEnumerator PlayCaughtSound()
    {
        StopTeaching();

        if (angrySound != null)
        {
            angryAudioSource.PlayOneShot(angrySound);

            yield return new WaitForSeconds(
                angrySound.length
            );
        }
    }
}