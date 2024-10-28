using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider healthSlider;

    private void Awake()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Destroy(gameObject);
            GameEvents.TriggerOnEnemyDead(this);
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = health;
    }
}
