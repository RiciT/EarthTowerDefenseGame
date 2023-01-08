using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject Canvas;

    public void StartAgain()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Canvas.SetActive(true);
    }
}
