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
       currentHealth = maxHealth;
       _healthBar.SetMaxHealth(maxHealth);
       _gameEndManager = FindObjectOfType<GameEndManager>();
    }
    
        public void TakeDamage(int damage)
        {

            if (currentHealth <= 0)
                return;
        
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
            Debug.Log(currentHealth);
            _healthBar.SetHealth(currentHealth);
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
                    _gameEndManager.Win();
                }
            }
        }
}
