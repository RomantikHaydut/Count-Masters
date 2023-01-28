using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float runSpeed = 4f;

    public int startCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            startCount = 10;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        FindObjectOfType<UIManager>().CloseStartPanel();
        FindObjectOfType<PlayerMagnet>().CreateRunnerStart();
        FindObjectOfType<PlayerMovement>().canMove = true;
        FindObjectOfType<PlayerMovement>().isGameStarted = true;
    }
    public void WinGame()
    {
        startCount = FindObjectOfType<PlayerMagnet>().activePlayerlist.Count / 3; // Price for next level.
        Camera.main.GetComponent<CameraController>().enabled = false;
        FindObjectOfType<PlayerMovement>().canMove = false;
        FindObjectOfType<UIManager>().OpenGamePanel();
    }

    public void LoseGame()
    {
        Camera.main.GetComponent<CameraController>().enabled = false;
        FindObjectOfType<PlayerMovement>().canMove = false;
        BossController bossController = FindObjectOfType<BossController>();
        if (bossController != null)
            bossController.BossWin();
        FindObjectOfType<UIManager>().OpenGameOverPanel();
    }
}
