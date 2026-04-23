using System;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
   public int shieldAmmount { get; private set; }

   [SerializeField] private int _shieldToAdd;
   [SerializeField] private AudioSource shieldSFX;

    [SerializeField] private HealthBar playerHealth;
    [SerializeField] private TurnManager turnManager;

    private void Start()
   {
      shieldAmmount= 0;
   }
    public void GainShield()
    {
        shieldAmmount += _shieldToAdd;
        shieldSFX.Play();

        Debug.Log($"[PlayerShield] Shield: {shieldAmmount}");

        playerHealth.ShowShieldVisual();

        turnManager.OnEnemyTurnStart.RemoveListener(RemoveShieldVisual);
        turnManager.OnEnemyTurnStart.AddListener(RemoveShieldVisual);
    }

    public int TakeDamage(int damage)
  {
     int absorbed = Mathf.Min(shieldAmmount, damage);
     shieldAmmount -= absorbed;

     return damage - absorbed; // leftover damage
  }

    void RemoveShieldVisual()
    {
        playerHealth.HideShieldVisual();
        turnManager.OnEnemyTurnStart.RemoveListener(RemoveShieldVisual);
    }
}
