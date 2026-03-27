using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] int _attackDamage;
   [SerializeField] int _specialAttackDamage;
   private EnemyHealth enemyHealth;
   private bool playerTurn;

   private void Awake()
   {
      enemyHealth = FindObjectOfType<EnemyHealth>();
   }
   
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.R))
      {
         DoDamage();
      }

      if (Input.GetKeyDown(KeyCode.S))
      {
         SpecialAttack();
      }
   }

   void DoDamage()
   {
      enemyHealth.TakeDamage(_attackDamage);
   }


   void SpecialAttack()
   {
      enemyHealth.TakeDamage(_specialAttackDamage);
   }
   
}
