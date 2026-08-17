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

    private Quaternion blackboardRotation;

    private void Start()
    {
        // Whatever direction the teacher is facing at the start
        // is considered the blackboard direction.
        blackboardRotation = transform.rotation;

        StartCoroutine(TeacherRoutine());
    }

    private IEnumerator TeacherRoutine()
    {
        while (true)
        {
            // =========================
            // TEACHING
            // =========================

            float teachingTime = Random.Range(
                teachingTimeMin,
                teachingTimeMax
            );

            yield return new WaitForSeconds(teachingTime);


            // =========================
            // TURN AROUND
            // =========================

            Quaternion centerRotation =
                blackboardRotation * Quaternion.Euler(0f, 180f, 0f);

            yield return RotateTo(
                centerRotation,
                turnSpeed
            );


            // =========================
            // PAUSE BEFORE SCANNING
            // =========================

            yield return new WaitForSeconds(pauseBeforeScan);


            // =========================
            // SCAN LEFT
            // =========================

            Quaternion leftRotation =
                centerRotation * Quaternion.Euler(0f, -scanAngle, 0f);

            yield return RotateTo(
                leftRotation,
                scanSpeed
            );


            // =========================
            // SCAN RIGHT
            // =========================

            Quaternion rightRotation =
                centerRotation * Quaternion.Euler(0f, scanAngle, 0f);

            yield return RotateTo(
                rightRotation,
                scanSpeed
            );


            // =========================
            // PAUSE AFTER SCAN
            // =========================

            yield return new WaitForSeconds(pauseAfterScan);


            // =========================
            // RETURN TO BLACKBOARD
            // =========================

            yield return RotateTo(
                blackboardRotation,
                turnSpeed
            );
        }
    }

    private IEnumerator RotateTo(
        Quaternion targetRotation,
        float speed
    )
    {
        while (
            Quaternion.Angle(
                transform.rotation,
                targetRotation
            ) > 0.5f
        )
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                speed * Time.deltaTime
            );

            yield return null;
        }

        transform.rotation = targetRotation;
    }
}