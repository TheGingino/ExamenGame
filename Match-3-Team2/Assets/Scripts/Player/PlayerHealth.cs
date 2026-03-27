using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _healAmount;
    private int currentHealth;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerShield _playerShield;

    private bool hasWon = false;
    private bool hasLost = false;
    
    private void Start()
    {
        currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        if (_playerShield.shieldAmmount > 0)
        {
            int remainingDamage = _playerShield.TakeDamage(damage);

            if (remainingDamage <= 0)
                return;

            damage = remainingDamage;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        _healthBar.SetHealth(currentHealth);
        Debug.Log(currentHealth  + " current health");
        CheckState();
    }


    public void Heal() // changed gainHealt to heal it was confusing for me srry
    {
        if (CombatMeter.Instance.UseCharge(TileType.Heal))
        {
            currentHealth = Mathf.Min(currentHealth + _healAmount, _maxHealth);
            _healthBar.SetHealth(currentHealth);
            Debug.Log("[PlayerHealth] Healed {_healAmount}. HP: {currentHealth}");
        }
    }
    
  public void CheckState()
    {
        if (currentHealth <= 0)
        {
            hasLost = true;
            Debug.Log("[PlayerHealth] LOST");
        }
    }
}
