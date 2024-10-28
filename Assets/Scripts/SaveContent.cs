using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveContent : MonoBehaviour
{
    public static SaveContent Instance { get; private set; }
    private const string PLAYER_PREFS_HIGHSCORE = "HighScore";
    private const string PLAYER_PREFS_COINS = "Coins";

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_HIGHSCORE, Score.Instance.GetScore());
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(PLAYER_PREFS_HIGHSCORE);
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_COINS, Coins.Instance.GetCoins());
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt(PLAYER_PREFS_COINS);
    }

    public void ResetAllSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
