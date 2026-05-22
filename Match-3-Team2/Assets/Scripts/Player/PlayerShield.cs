using System;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
   public int ShieldAmmount { get; private set; }

    [SerializeField] private int _shieldToAdd;
    [SerializeField] private AudioSource _shieldSFX;

    [SerializeField] private HealthBar _playerHealth;
    [SerializeField] private TurnManager _turnManager;

    private void Start()
   {
      ShieldAmmount= 0;
   }
    public void GainShield()
    {
        ShieldAmmount += _shieldToAdd;
        _shieldSFX.Play();

        Debug.Log($"[PlayerShield] Shield: {ShieldAmmount}");

        _playerHealth.ShowShieldVisual();
    }

    public int TakeDamage(int damage)
    {
        int absorbed = Mathf.Min(ShieldAmmount, damage);
        ShieldAmmount -= absorbed;

        Debug.Log($"[PlayerShield] Shield after damage: {ShieldAmmount}");

        if (ShieldAmmount <= 0)
        {
            _playerHealth.HideShieldVisual();
        }

        return damage - absorbed;
    }

    void RemoveShieldVisual()
    {
        _playerHealth.HideShieldVisual();
        _turnManager.OnEnemyTurnStart.RemoveListener(RemoveShieldVisual);
    }
}
