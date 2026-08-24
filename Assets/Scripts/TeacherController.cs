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

    [Header("Turn Direction")]
    [SerializeField] private float turnDirection = -1f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private bool isFacingBoard = true;
    public bool IsFacingBoard => isFacingBoard;

    private Quaternion blackboardRotation;

    private void Start()
    {
        isFacingBoard = true;
        blackboardRotation = transform.rotation;
        StartCoroutine(TeacherRoutine());
    }

    private IEnumerator TeacherRoutine()
    {
        while (true)
        {
            isFacingBoard = true;

            float teachingTime = Random.Range(
                teachingTimeMin,
                teachingTimeMax
            );

            yield return new WaitForSeconds(teachingTime);

            isFacingBoard = false;

            animator.SetTrigger("TurnAround");

            yield return RotateBy(
                180f * -turnDirection,
                turnSpeed
            );

            yield return new WaitForSeconds(pauseBeforeScan);

            yield return RotateBy(-scanAngle, scanSpeed);
            yield return RotateBy(scanAngle, scanSpeed);
            yield return RotateBy(scanAngle, scanSpeed);
            yield return RotateBy(-scanAngle, scanSpeed);

            yield return new WaitForSeconds(pauseAfterScan);

            animator.SetTrigger("ReturnToBoard");

            yield return RotateBy(
                180f * turnDirection,
                turnSpeed
            );

            transform.rotation = blackboardRotation;

            isFacingBoard = true;
        }
    }

    private IEnumerator RotateBy(float angleDegrees, float speed)
    {
        float rotated = 0f;
        float target = Mathf.Abs(angleDegrees);
        float sign = Mathf.Sign(angleDegrees);

        while (rotated < target)
        {
            float step = Mathf.Min(
                speed * Time.deltaTime,
                target - rotated
            );

            transform.Rotate(
                0f,
                sign * step,
                0f,
                Space.Self
            );

            rotated += step;

            yield return null;
        }
    }

    public void StopTeacher()
    {
        StopAllCoroutines();
    }
}