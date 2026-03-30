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
     

      if (Input.GetKeyDown(KeyCode.S))
      {
         SpecialAttack();
      }
   }

   public void DoDamage()
   {
      enemyHealth.TakeDamage(_attackDamage);
      Debug.Log("[PlayerAttack] Damage used! " + _attackDamage);
   }


   public void SpecialAttack()
   {
         enemyHealth.TakeDamage(_specialAttackDamage);
         Debug.Log("[PlayerAttack] Special used! -{_specialAttackDamage}");
   }
   
}
