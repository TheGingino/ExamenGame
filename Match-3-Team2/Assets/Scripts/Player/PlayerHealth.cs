using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _healAmount;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerShield _playerShield;
    [SerializeField] private AudioSource _healSFX;
    [SerializeField] private Animator _animator;

    private int _currentHealth;

    private GameEndManager _gameEndManager;
    private ImageFader _imageFader;
    private bool _hasLost = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _gameEndManager = FindObjectOfType<GameEndManager>();
        _imageFader = FindObjectOfType<ImageFader>();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        if (_playerShield.ShieldAmmount > 0)
        {
            int remainingDamage = _playerShield.TakeDamage(damage);

            if (remainingDamage <= 0) return;

            damage = remainingDamage;
        }
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        _healthBar.SetHealth(_currentHealth);
        _imageFader.ShowDamage();
        Debug.Log(_currentHealth + " current health");

        CheckState();
    }


    public void Heal() 
    {
            _currentHealth = Mathf.Min(_currentHealth + _healAmount, _maxHealth);
            _healthBar.SetHealth(_currentHealth);
            _healSFX.Play();
            _animator.SetTrigger("heal");
            Debug.Log("[PlayerHealth] Healed {_healAmount}. HP: {currentHealth}");
    }

    private void CheckState()
    {
        if (_currentHealth <= 0 && !_hasLost)
        {
            _hasLost = true;

            if (_gameEndManager != null)
            {
                StartCoroutine(WaitForLoss());
            }
        }
    }
    
    IEnumerator WaitForLoss()
    {
        yield return new WaitForSeconds(2f);
        _gameEndManager.TriggerLose();
    }
}
