using UnityEngine;

public class TeacherVision : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PhoneController phoneController;

    [SerializeField] private float visionDistance = 15f;
    [SerializeField] private float visionAngle = 90f;

    private void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        float distance = directionToPlayer.magnitude;

        if (distance > visionDistance)
            return;

        float angle = Vector3.Angle(
            transform.forward,
            directionToPlayer
        );

        if (angle > visionAngle / 2f)
            return;

        // Player is inside the teacher's vision cone

        if (phoneController.IsPhoneOut)
        {
            Debug.Log("📱 PHONE OUT — TEACHER CAN SEE YOU!");
        }
        else
        {
            Debug.Log("Phone hidden — you're safe.");
        }
    }
}