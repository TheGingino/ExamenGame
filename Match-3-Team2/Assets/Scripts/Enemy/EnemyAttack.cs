using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private bool _enemyTurn;
   [SerializeField] Animator animator;


    private void Awake()
    {
       playerHealth=FindObjectOfType<PlayerHealth>();
    }
    
    void DoDamage()
    {
        int attackIndex = Random.Range(1, 4); 
        animator.SetTrigger("Attack_" + attackIndex);
        int roll = Random.Range(0, 100);

        int damage;

        if (roll < 50) damage = 1;      // 50% chance
        else if (roll < 75) damage = 2; // 25%
        else if (roll < 90) damage = 3; // 15%
        else damage = 4;                // 10%
        Debug.Log( "damage" + damage);  
        playerHealth.TakeDamage(damage);
    }
}
