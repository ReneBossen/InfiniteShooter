using System;

using UnityEngine;

public static class GameEvents
{
    public static event Action<Enemy> OnEnemyDead;
    public static event EventHandler OnPlayerDead;

    public static void TriggerOnEnemyDead(EnemyHealth enemyHealth)
    {
        if (enemyHealth != null)
        {
            Enemy enemyComponent = enemyHealth.gameObject.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                OnEnemyDead?.Invoke(enemyComponent);
            }
            else
            {
                Debug.LogWarning("Enemy component is null.");
            }
        }
        else
        {
            Debug.LogWarning("EnemyHealth object is null.");
        }
    }

    public static void TriggerOnPlayerDead(PlayerHealth player)
    {
        OnPlayerDead?.Invoke(player, EventArgs.Empty);
    }
}
