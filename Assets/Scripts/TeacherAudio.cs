using UnityEngine;
using System.Collections;

public class TeacherAudio : MonoBehaviour
{
    [Header("Teacher Talking")]
    [SerializeField] private AudioClip[] talkingSounds;
    [SerializeField] private float minDelay = 0.5f;
    [SerializeField] private float maxDelay = 2f;

    [Header("Teacher Angry")]
    [SerializeField] private AudioClip angrySound;

    private AudioSource audioSource;
    private bool teacherCaught = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(TalkingRoutine());
    }

    private IEnumerator TalkingRoutine()
    {
        while (!teacherCaught)
        {
            if (talkingSounds.Length > 0)
            {
                AudioClip randomSound =
                    talkingSounds[
                        Random.Range(0, talkingSounds.Length)
                    ];

                audioSource.clip = randomSound;

                audioSource.Play();

                yield return new WaitForSeconds(
                    randomSound.length
                );

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

    public void StopTalking()
    {
        teacherCaught = true;

        audioSource.Stop();
    }

    public IEnumerator PlayCaughtSound()
    {
        StopTalking();

        if (angrySound != null)
        {
            audioSource.PlayOneShot(angrySound);

            yield return new WaitForSeconds(
                angrySound.length
            );
        }
    }
}