using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        Debug.Log("💀 GAME OVER — YOU GOT CAUGHT!");
        Time.timeScale = 0f;
    }
}