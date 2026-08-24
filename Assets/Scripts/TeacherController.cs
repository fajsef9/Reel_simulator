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
    [SerializeField] private float turnSpeed = 35f;   // degrees per second
    [SerializeField] private float scanSpeed = 20f;   // degrees per second

    [Header("Turn Direction")]
    [Tooltip("The direction she turns to face the class, leading with her right side. " +
             "+1 or -1 depending on how your model's forward axis is set up — flip this if she turns the wrong way.")]
    [SerializeField] private float turnDirection = -1f;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private bool isFacingBoard = true;
    public bool IsFacingBoard => isFacingBoard;

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

            // Turn toward the class, leading with her right side (turnDirection controls this)
            animator.SetTrigger("TurnAround");
            yield return RotateBy(180f * -turnDirection, turnSpeed);

            // Wait before scanning
            yield return new WaitForSeconds(pauseBeforeScan);

            // Scan right, then back to center, then left, then back to center
            yield return RotateBy(-scanAngle, scanSpeed);
            yield return RotateBy(scanAngle, scanSpeed);   // back to center
            yield return RotateBy(scanAngle, scanSpeed);
            yield return RotateBy(-scanAngle, scanSpeed);  // back to center

            // Wait after scanning
            yield return new WaitForSeconds(pauseAfterScan);

            // Turn back to the blackboard, continuing in the SAME direction she turned initially
            // (not reversing) so the motion reads as one continuous, natural turn.
            animator.SetTrigger("ReturnToBoard");
            yield return RotateBy(180f * turnDirection, turnSpeed);

            // Snap-correct any tiny float drift so she's exactly facing the board again
            transform.rotation = blackboardRotation;
        }
    }

    private IEnumerator RotateBy(float angleDegrees, float speed)
    {
        float rotated = 0f;
        float target = Mathf.Abs(angleDegrees);
        float sign = Mathf.Sign(angleDegrees);

        while (rotated < target)
        {
            float step = Mathf.Min(speed * Time.deltaTime, target - rotated);
            transform.Rotate(0f, sign * step, 0f, Space.Self);
            rotated += step;
            yield return null;
        }
    }

    public void StopTeacher()
    {
        StopAllCoroutines();
    }
}