using UnityEngine;
using UnityEngine.UI;

public class BrainrotManager : MonoBehaviour
{
    [SerializeField] private Slider brainrotBar;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float maxBrainrot = 100f;
    [SerializeField] private float drainRate = 2f;

    private float currentBrainrot;
    private bool gameOver = false;

    private void Start()
    {
        currentBrainrot = maxBrainrot;

        brainrotBar.maxValue = maxBrainrot;
        brainrotBar.value = currentBrainrot;
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
            return;

        currentBrainrot -= drainRate * Time.deltaTime;

        currentBrainrot = Mathf.Clamp(
            currentBrainrot,
            0f,
            maxBrainrot
        );

        brainrotBar.value = currentBrainrot;

        if (currentBrainrot <= 0f && !gameOver)
        {
            gameOver = true;
            gameManager.GameOver();
        }
    }

    public void RestoreBrainrot(float amount)
    {
        if (gameManager.IsGameOver)
            return;

        currentBrainrot += amount;

        currentBrainrot = Mathf.Clamp(
            currentBrainrot,
            0f,
            maxBrainrot
        );

        brainrotBar.value = currentBrainrot;
    }
}