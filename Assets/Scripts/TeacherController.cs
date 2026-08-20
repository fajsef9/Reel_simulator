using UnityEngine;
using System.Collections;

public class TeacherController : MonoBehaviour
{
    [SerializeField] private float scanAngle = 60f;

    [Header("Timing")]
    [SerializeField] private float teachingTimeMin = 5f;
    [SerializeField] private float teachingTimeMax = 20f;
    [SerializeField] private float pauseBeforeScan = 1.5f;
    [SerializeField] private float pauseAfterScan = 1f;

    [Header("Movement")]
    [SerializeField] private float turnSpeed = 35f;
    [SerializeField] private float scanSpeed = 20f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private Quaternion blackboardRotation;

    private void Start()
    {
        blackboardRotation = transform.rotation;
        StartCoroutine(TeacherRoutine());
    }

    private IEnumerator TeacherRoutine()
    {
        while (true)
        {
            // Teach while facing the blackboard
            float teachingTime = Random.Range(teachingTimeMin, teachingTimeMax);
            yield return new WaitForSeconds(teachingTime);

            // Turn toward the class
            animator.SetTrigger("TurnAround");

            Quaternion centerRotation = blackboardRotation * Quaternion.Euler(0f, -180f, 0f);
            yield return RotateTo(centerRotation, turnSpeed);

            // Wait before scanning
            yield return new WaitForSeconds(pauseBeforeScan);

            // Scan right
            Quaternion rightRotation = centerRotation * Quaternion.Euler(0f, -scanAngle, 0f);
            yield return RotateTo(rightRotation, scanSpeed);

            // Scan left
            Quaternion leftRotation = centerRotation * Quaternion.Euler(0f, scanAngle, 0f);
            yield return RotateTo(leftRotation, scanSpeed);

            // Wait after scanning
            yield return new WaitForSeconds(pauseAfterScan);

            // Return to the blackboard
            animator.SetTrigger("ReturnToBoard");
            yield return RotateTo(blackboardRotation, turnSpeed);
        }
    }

    private IEnumerator RotateTo(Quaternion targetRotation, float speed)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
    public void StopTeacher()
    {
        StopAllCoroutines();
    }
}