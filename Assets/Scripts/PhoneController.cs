using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private Vector3 hiddenPosition;
    [SerializeField] private Vector3 pulledOutPosition;
    [SerializeField] private float pullSpeed = 8f;

    private bool phoneOut = false;

    public bool IsPhoneOut => phoneOut;

    void Start()
    {
        transform.localPosition = hiddenPosition;
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            phoneOut = !phoneOut;
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