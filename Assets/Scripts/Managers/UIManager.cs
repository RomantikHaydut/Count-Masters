using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject gameOverPanel;
    private void Awake()
    {
        if (gamePanel.activeInHierarchy)
            gamePanel.SetActive(false);

        if (gameOverPanel.activeInHierarchy)
            gameOverPanel.SetActive(false);


        gameStartPanel.SetActive(true);

    }
    public void DisplayText(GameObject canvas, string text)
    {
        TMP_Text text_ = canvas.GetComponentInChildren<TMP_Text>();
        text_.text = text;
    }

    public void CloseStartPanel() => gameStartPanel.SetActive(false);
    public void OpenGamePanel()
    {
        string scoreText = "Your Score : " + ScoreManager.Instance.score.ToString();
        DisplayText(gamePanel, scoreText);
        gamePanel.SetActive(true);
    }

    public void OpenGameOverPanel() => gameOverPanel.SetActive(true);
}
