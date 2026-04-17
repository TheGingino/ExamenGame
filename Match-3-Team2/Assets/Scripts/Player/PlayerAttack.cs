using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] int _attackDamage;
   [SerializeField] int _specialAttackDamage;
   [SerializeField] Animator animator;
   private EnemyHealth enemyHealth;
   private bool playerTurn;

   private void Awake()
   {
      enemyHealth = FindObjectOfType<EnemyHealth>();
   }
   
   public void DoDamage()
   {
      enemyHealth.TakeDamage(_attackDamage);
      animator.SetTrigger("Hit_Small");
      Debug.Log("[PlayerAttack] Damage used! " + _attackDamage);
   }

   public void SpecialAttack()
   {
         enemyHealth.TakeDamage(_specialAttackDamage);
         animator.SetTrigger("Hit_Big");
         Debug.Log("[PlayerAttack] Special used! -{_specialAttackDamage}");
   }
   
}
