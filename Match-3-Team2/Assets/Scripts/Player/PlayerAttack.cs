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
