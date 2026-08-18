using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private Vector3 hiddenPosition;
    [SerializeField] private Vector3 pulledOutPosition;
    [SerializeField] private float pullSpeed = 8f;
    [SerializeField] private ReelManager reelManager;

    private bool phoneOut = false;

    public bool IsPhoneOut => phoneOut;
    [SerializeField] private BrainrotManager brainrotManager;
    [SerializeField] private float brainrotRestoreRate = 8f;

    void Start()
    {
        transform.localPosition = hiddenPosition;
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            phoneOut = !phoneOut;

            if (phoneOut)
            {
                reelManager.PlayRandomReel();
            }
        }
        if (phoneOut)
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