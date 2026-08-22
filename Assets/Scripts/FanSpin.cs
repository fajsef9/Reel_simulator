using UnityEngine;

public class FanSpin : MonoBehaviour
{
    [SerializeField] private Transform fan1;
    [SerializeField] private Transform fan2;
    [SerializeField] float speed = 300f;

    private void Update()
    {
        fan1.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
        fan2.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }
}