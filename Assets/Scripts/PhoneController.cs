using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Vector3 hiddenPosition;
    [SerializeField] private Vector3 pulledOutPosition;
    [SerializeField] private float pullSpeed = 8f;
    private bool phoneOut = false;
    void Start()
    {
        transform.localPosition = hiddenPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            phoneOut = !phoneOut;
        }

        Vector3 targetPosition;

        if (phoneOut){
            targetPosition = pulledOutPosition;
        }
        else{
            targetPosition = hiddenPosition;
        }
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            pullSpeed * Time.deltaTime
        );

    }
}
