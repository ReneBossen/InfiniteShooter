using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
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
            GameEvents.TriggerOnPlayerDead(this);
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = health;
    }
}
