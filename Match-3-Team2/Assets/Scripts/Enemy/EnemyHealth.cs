using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
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
        }
    }
}
