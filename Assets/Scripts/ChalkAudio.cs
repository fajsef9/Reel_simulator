using UnityEngine;

public class ChalkAudio : MonoBehaviour
{
    [SerializeField] private TeacherController teacherController;
    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        if (teacherController.IsFacingBoard)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}