using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Game Systems")]
    [SerializeField] private ReelManager reelManager;

    private bool gameOver = false;

    public bool IsGameOver => gameOver;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("💀 GAME OVER!");

        // Show final score
        finalScoreText.text =
            "SCORE: " + reelManager.CurrentScore;

        // Show Game Over screen
        gameOverPanel.SetActive(true);

        // Freeze the game
        Time.timeScale = 0f;
    }
}