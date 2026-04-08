using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
[SerializeField] private int maxHealth;
private int currentHealth;
   
    private GameEndManager _gameEndManager;

    private void Start()
    {
       currentHealth = maxHealth;
       _gameEndManager = FindObjectOfType<GameEndManager>();
    }
    
        public void TakeDamage(int damage)
        {

            if (currentHealth <= 0)
                return;
        
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
            Debug.Log(currentHealth);
            CheckState();
        }

        public void CheckState()
        {
            if (currentHealth <= 0)
            {
                Debug.Log("Won");

                if (_gameEndManager != null)
                {
                    _gameEndManager.Win();
                }
            }
        }
}
