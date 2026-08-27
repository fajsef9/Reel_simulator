using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverMessage;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Game Systems")]
    [SerializeField] private ReelManager reelManager;
    [SerializeField] private GameOverStamp gameOverStamp;
    [SerializeField] private TeacherAudio teacherAudio;
    [SerializeField] private AudioSource brainrotAudioSource;
    [SerializeField] private AudioClip brainrotEndSound;

    private bool gameOver = false;

    private string[] brainrotMessages =
    {
        "DON'T STOP SCROLLING EVER!!!!",
        "You just missed the next trending reel",
        "Stop paying attention in class nerd"
    };

    private string[] teacherMessages =
    {
        "Phone gone. Aura gone.",
        "The teacher read all your texts",
        "Might have to visit her cabin later ;)",
        "Caught reel handed"
    };

    public bool IsGameOver => gameOver;

    private void Start()
    {
        Time.timeScale = 1f;

        gameOver = false;

        gameOverPanel.SetActive(false);
    }

    public void GameOverBrainrot()
    {
        if (gameOver)
            return;

        gameOver = true;

        teacherAudio.StopTeaching();

        reelManager.StopVideo();

        StartCoroutine(BrainrotGameOverRoutine());
    }
    private IEnumerator BrainrotGameOverRoutine()
    {
        if (brainrotEndSound != null)
        {
            brainrotAudioSource.PlayOneShot(brainrotEndSound);

            yield return new WaitForSecondsRealtime(
                brainrotEndSound.length
            );
        }

        ShowRandomMessage(brainrotMessages);

        finalScoreText.text =
            "SCORE: " + reelManager.CurrentScore;

        gameOverPanel.SetActive(true);

        gameOverStamp.Stamp();

        Time.timeScale = 0f;
    }

    public void GameOverTeacherCaught()
    {
        ShowRandomMessage(teacherMessages);

        finalScoreText.text =
            "SCORE: " + reelManager.CurrentScore;

        gameOverPanel.SetActive(true);

        gameOverStamp.Stamp();

        Time.timeScale = 0f;
    }
    public void StopGame()
    {
        if (gameOver)
            return;

        gameOver = true;
    }

    private void ShowRandomMessage(string[] messages)
    {
        int randomIndex =
            Random.Range(0, messages.Length);

        gameOverMessage.text =
            messages[randomIndex];
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

        SceneManager.LoadScene("Home");
    }
}