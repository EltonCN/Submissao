using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverInterface gameOverInterface;
    [SerializeField] private string mainMenuScene;

    public void GameOver()
    {
        gameOverInterface.Setup();
    }

    public void tryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
