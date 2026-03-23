using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage;

    private PlayerHealth playerHealth;
    private bool _enemyTurn;


    private void Awake()
    {
       playerHealth=FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoDamage();
            Debug.Log("DAMANGE");
        }
    }

    void DoDamage()
    {
        playerHealth.TakeDamage(attackDamage);
    }
}
