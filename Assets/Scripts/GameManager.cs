using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;

    public bool IsGameOver => gameOver;

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("💀 GAME OVER!");

        Time.timeScale = 0f;
    }
}