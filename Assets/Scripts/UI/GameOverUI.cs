using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private int highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
    }
    private void Start()
    {
        GameEvents.OnPlayerDead += Player_OnPlayerDead;
        Hide();
        highScore = SaveContent.Instance.GetHighScore();
    }

    private void Player_OnPlayerDead(object sender, EventArgs e)
    {
        Debug.Log("ON PLAYER DEAD KALDT FRA GAMEOVERUI");
        gameObject.SetActive(true);
        if (Score.Instance.GetScore() > highScore)
        {
            SaveContent.Instance.SaveHighScore();
            highScore = Score.Instance.GetScore();
        }


        scoreText.text = "Final score: " + Score.Instance.GetScore();
        highScoreText.text = "Highscore: " + highScore;

        WeaponRotation.Instance.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameEvents.OnPlayerDead -= Player_OnPlayerDead;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenShop()
    {
        ShopUI.Instance.Show();
        Hide();
    }

    public void ResetAllSaves()
    {
        SaveContent.Instance.ResetAllSaves();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
