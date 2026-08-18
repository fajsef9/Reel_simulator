using UnityEngine;

public class TeacherVision : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PhoneController phoneController;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float visionDistance = 15f;
    [SerializeField] private float visionAngle = 90f;

    [SerializeField] private float catchDelay = 0.5f;

    private float catchTimer = 0f;

    private void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        float distance = directionToPlayer.magnitude;

        // Player too far away
        if (distance > visionDistance)
        {
            catchTimer = 0f;
            return;
        }

        float angle = Vector3.Angle(
            transform.forward,
            directionToPlayer
        );

        // Player outside vision cone
        if (angle > visionAngle / 2f)
        {
            catchTimer = 0f;
            return;
        }

        // Player is inside vision cone
        if (phoneController.IsPhoneOut)
        {
            catchTimer += Time.deltaTime;

            if (catchTimer >= catchDelay)
            {
                gameManager.GameOver();
            }
        }
        else
        {
            // Phone hidden = safe
            catchTimer = 0f;
        }
    }
}