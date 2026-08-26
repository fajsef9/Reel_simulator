using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private Vector3 hiddenPosition;
    [SerializeField] private Vector3 pulledOutPosition;
    [SerializeField] private float pullSpeed = 8f;
    [SerializeField] private ReelManager reelManager;
    [SerializeField] private BrainrotManager brainrotManager;
    [SerializeField] private float brainrotRestoreRate = 8f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pullOutSound;
    [SerializeField] private AudioClip hideSound;

    private bool phoneOut = false;

    public bool IsPhoneOut => phoneOut;

    private void Start()
    {
        transform.localPosition = hiddenPosition;
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            phoneOut = !phoneOut;

            if (phoneOut)
            {
                if (pullOutSound != null)
                {
                    audioSource.PlayOneShot(pullOutSound);
                }

                reelManager.PlayRandomReel();
            }
            else
            {
                if (hideSound != null)
                {
                    audioSource.PlayOneShot(hideSound);
                }
            }
        }

        if (phoneOut && reelManager.IsVideoPlaying)
        {
            brainrotManager.RestoreBrainrot(
                brainrotRestoreRate * Time.deltaTime
            );
        }

        Vector3 targetPosition;

        if (phoneOut)
        {
            targetPosition = pulledOutPosition;
        }
        else
        {
            targetPosition = hiddenPosition;
        }

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            pullSpeed * Time.deltaTime
        );
    }
}