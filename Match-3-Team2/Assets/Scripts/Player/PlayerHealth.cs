using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerShield _playerShield;

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

        if (_playerShield.shieldAmmount > 0)
        {
            int remainingDamage = _playerShield.TakeDamage(damage);

            if (remainingDamage <= 0)
                return;

            damage = remainingDamage;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        _healthBar.SetHealth(currentHealth);
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
