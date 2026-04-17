using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] Animator animator;
    private int _currentHealth;
    
    [SerializeField] private HealthBar healthBar;

    private GameEndManager _gameEndManager;
   
    private void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _gameEndManager = FindObjectOfType<GameEndManager>();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        Debug.Log(_currentHealth);
        healthBar.SetHealth(_currentHealth);

        CheckState();
    }

        public void CheckState()
        {
            if (_currentHealth <= 0)
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