using UnityEngine;
using UnityEngine.UI;

public class BrainrotManager : MonoBehaviour
{
    [SerializeField] private Slider brainrotBar;

    [SerializeField] private float maxBrainrot = 100f;
    [SerializeField] private float drainRate = 2f;

    private float currentBrainrot;

    private void Start()
    {
        currentBrainrot = maxBrainrot;

        brainrotBar.maxValue = maxBrainrot;
        brainrotBar.value = currentBrainrot;
    }

    private void Update()
    {
        currentBrainrot -= drainRate * Time.deltaTime;

        currentBrainrot = Mathf.Clamp(
            currentBrainrot,
            0f,
            maxBrainrot
        );

        brainrotBar.value = currentBrainrot;
    }
    public void RestoreBrainrot(float amount)
    {
        currentBrainrot += amount;

        currentBrainrot = Mathf.Clamp(
            currentBrainrot,
            0f,
            maxBrainrot
        );

        brainrotBar.value = currentBrainrot;
    }
}