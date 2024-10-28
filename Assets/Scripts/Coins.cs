using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static Coins Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI coinsText;
    private int coins;

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
        coins = SaveContent.Instance.GetCoins();
        UpdateCoinsText();
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyDead += GameManager_OnEnemyDead;
        GameEvents.OnPlayerDead += Player_OnPlayerDead;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDead -= GameManager_OnEnemyDead;
        GameEvents.OnPlayerDead -= Player_OnPlayerDead;
    }
    private void GameManager_OnEnemyDead(Enemy enemy)
    {
        AddCoins(enemy.GetMoneyDropAmount());
    }

    private void Player_OnPlayerDead(object sender, EventArgs e)
    {
        SaveCoins();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsText();
    }

    public void SpendCoins(int amount)
    {
        coins -= amount;
        UpdateCoinsText();
        SaveCoins();
    }

    public void SaveCoins()
    {
        SaveContent.Instance.SaveCoins();
    }

    public int GetCoins()
    {
        return coins;
    }

    private void UpdateCoinsText()
    {
        coinsText.text = "Coins: " + coins;
    }
}
