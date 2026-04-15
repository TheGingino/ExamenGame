using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    private GameEndManager _gameEndManager;
   
    private void Start()
    {
        _currentHealth = _maxHealth;
        _gameEndManager = FindObjectOfType<GameEndManager>();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        Debug.Log(_currentHealth);

        CheckState();
    }

    private void CheckState()
    {
        if (_currentHealth <= 0)
        {
            if (_gameEndManager != null)
            {
                _gameEndManager.TriggerWin();
            }
        }
    }
}