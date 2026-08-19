using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 1f;

        gameOver = false;

        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("💀 GAME OVER!");

        finalScoreText.text =
            "SCORE: " + reelManager.CurrentScore;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }



    public void Retry()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }


    public void GoToHome()
    {
        Time.timeScale = 1f;


        Debug.Log("Going to Home");
    }
}