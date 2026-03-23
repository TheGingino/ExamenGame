using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [SerializeField] private HealthBar _healthBar;

    private bool hasWon = false;
    private bool hasLost = false;


    private void Start()
    {
        currentHealth = maxHealth;
        _healthBar.SetMaxHealth(maxHealth);
    }

  public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;
        
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        _healthBar.SetHealth(currentHealth);
        Debug.Log(currentHealth);
        CheckState();
    }
    
  public void CheckState()
    {
        if (currentHealth <= 0)
        {
            hasLost = true;
            Debug.Log("LOST");
        }
    }
}
