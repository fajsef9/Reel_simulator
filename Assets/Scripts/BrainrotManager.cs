using UnityEngine;
using UnityEngine.UI;

public class BrainrotManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider brainrotBar;
    [SerializeField] private LowBrainrotWarning lowBrainrotWarning;

    [Header("Game")]
    [SerializeField] private GameManager gameManager;

    [Header("Brainrot Settings")]
    [SerializeField] private float maxBrainrot = 100f;
    [SerializeField] private float drainRate = 2f;

    [Header("Brainrot Colors")]
    [SerializeField] private Color fullColor = Color.green;
    [SerializeField] private Color emptyColor = Color.red;

    private float currentBrainrot;
    private bool gameOver = false;

    private Image fillImage;

    private void Start()
    {
        currentBrainrot = maxBrainrot;

        brainrotBar.maxValue = maxBrainrot;
        brainrotBar.value = currentBrainrot;

        fillImage = brainrotBar.fillRect
            .GetComponent<Image>();

        UpdateBrainrotColor();
        UpdateLowBrainrotWarning();
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
            return;

        currentBrainrot -=
            drainRate * Time.deltaTime;

        currentBrainrot = Mathf.Clamp(
            currentBrainrot,
            0f,
            maxBrainrot
        );

        brainrotBar.value = currentBrainrot;

        UpdateBrainrotColor();
        UpdateLowBrainrotWarning();

        if (currentBrainrot <= 0f && !gameOver)
        {
            gameOver = true;

            gameManager.GameOverBrainrot();
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

        UpdateBrainrotColor();
        UpdateLowBrainrotWarning();
    }

    private void UpdateBrainrotColor()
    {
        if (fillImage == null)
            return;

        float percentage =
            currentBrainrot / maxBrainrot;

        fillImage.color = Color.Lerp(
            emptyColor,
            fullColor,
            percentage
        );
    }

    private void UpdateLowBrainrotWarning()
    {
        if (lowBrainrotWarning == null)
            return;

        float normalizedBrainrot =
            currentBrainrot / maxBrainrot;

        lowBrainrotWarning.UpdateBrainrot(
            normalizedBrainrot
        );
    }
}