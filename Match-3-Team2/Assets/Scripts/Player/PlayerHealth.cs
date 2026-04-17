using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _healAmount;
    private int currentHealth;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerShield _playerShield;

    private int _currentHealth;

    private GameEndManager _gameEndManager;
    private bool _hasLost = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _gameEndManager = FindObjectOfType<GameEndManager>();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        if (_playerShield.shieldAmmount > 0)
        {
            int remainingDamage = _playerShield.TakeDamage(damage);

            if (remainingDamage <= 0) return;

            damage = remainingDamage;
        }

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        _healthBar.SetHealth(_currentHealth);

        Debug.Log(_currentHealth + " current health");

        CheckState();
    }


    public void Heal() 
    {
            currentHealth = Mathf.Min(currentHealth + _healAmount, _maxHealth);
            _healthBar.SetHealth(currentHealth);
            Debug.Log("[PlayerHealth] Healed {_healAmount}. HP: {currentHealth}");
    }

    private void CheckState()
    {
        if (_currentHealth <= 0 && !_hasLost)
        {
            _hasLost = true;

            if (_gameEndManager != null)
            {
                _gameEndManager.TriggerLose();
            }
        }
    }
}
