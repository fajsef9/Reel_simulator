using UnityEngine;
using System.Collections;

public class TeacherVision : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PhoneController phoneController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TeacherController teacherController;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Vision")]
    [SerializeField] private float visionDistance = 15f;
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private float catchDelay = 0.5f;

    [Header("Game Over")]
    [SerializeField] private float angryAnimationTime = 1.5f;

    private float catchTimer = 0f;
    private bool hasCaughtPlayer = false;

    private void Update()
    {
        if (gameManager.IsGameOver || hasCaughtPlayer)
            return;

        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        // Player is too far away
        if (distance > visionDistance)
        {
            catchTimer = 0f;
            return;
        }

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // Player is outside the vision cone
        if (angle > visionAngle / 2f)
        {
            catchTimer = 0f;
            return;
        }

        // Check if the phone is visible
        if (phoneController.IsPhoneOut)
        {
            catchTimer += Time.deltaTime;

            if (catchTimer >= catchDelay)
            {
                hasCaughtPlayer = true;
                StartCoroutine(CatchPlayer());
            }
        }
        else
        {
            catchTimer = 0f;
        }
    }

    private IEnumerator CatchPlayer()
    {
        // Stop the teacher from continuing the scan
        teacherController.StopTeacher();

        // Play angry animation
        animator.SetTrigger("Angry");

        // Let the animation play
        yield return new WaitForSeconds(angryAnimationTime);

        // End the game
        gameManager.GameOverTeacherCaught();
    }
}