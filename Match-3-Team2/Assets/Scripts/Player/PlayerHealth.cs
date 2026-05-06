using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _healAmount;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerShield _playerShield;
    [SerializeField] private AudioSource healSFX;

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
            _currentHealth = Mathf.Min(_currentHealth + _healAmount, _maxHealth);
            _healthBar.SetHealth(_currentHealth);
            healSFX.Play();
            Debug.Log("[PlayerHealth] Healed {_healAmount}. HP: {currentHealth}");
    }

    private void CheckState()
    {
        if (_currentHealth <= 0 && !_hasLost)
        {
            _hasLost = true;

            if (_gameEndManager != null)
            {
                StartCoroutine(WaitForLos());
            }
        }
    }
    
    IEnumerator WaitForLos()
    {
        yield return new WaitForSeconds(2f);
        _gameEndManager.TriggerLose();
    }
}
