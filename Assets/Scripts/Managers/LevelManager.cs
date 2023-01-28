using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void StartLevel() => GameManager.Instance.StartGame();
    public void StartGame() => SceneManager.LoadScene(0);
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void Nextlevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            StartGame();
    }
}
