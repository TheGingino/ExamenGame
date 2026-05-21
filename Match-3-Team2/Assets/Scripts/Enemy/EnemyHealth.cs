using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _deathSFX;
    [SerializeField] private HealthBar _healthBar;

    public int _currentHealth;
    private GameEndManager _gameEndManager;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _gameEndManager = FindObjectOfType<GameEndManager>();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        Debug.Log(_currentHealth);
        _healthBar.SetHealth(_currentHealth);

        CheckState();
    }

    public void CheckState()
    {
        if (_currentHealth <= 0)
        {
            _animator.SetTrigger("Boss_Death");
            StartCoroutine(AudioEffect());
            Debug.Log("Won");

            if (_gameEndManager != null)
            {
                StartCoroutine(WinAfterAnim());
            }
        }
    }

    private IEnumerator AudioEffect()
    {
        yield return new WaitForSeconds(0.2f);
        _deathSFX.Play();
    }

    private IEnumerator WinAfterAnim()
    {
        yield return new WaitForSeconds(3f);
        _gameEndManager.TriggerWin();
    }
}