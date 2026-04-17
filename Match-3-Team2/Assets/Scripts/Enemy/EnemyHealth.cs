using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] Animator animator;
    private int currentHealth;
    
    [SerializeField] private HealthBar _healthBar;

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

        public void CheckState()
        {
            if (currentHealth <= 0)
            {
                animator.SetTrigger("Boss_Death");
                Debug.Log("Won");

                if (_gameEndManager != null)
                {
                   _gameEndManager.TriggerWin();
                }
            }
        }
}