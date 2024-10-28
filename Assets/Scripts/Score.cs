using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyDead += GameManager_OnEnemyDead;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDead -= GameManager_OnEnemyDead;
    }
    private void GameManager_OnEnemyDead(Enemy enemy)
    {
        AddScore();
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
}
