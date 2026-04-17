using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] int _attackDamage;
   [SerializeField] int _specialAttackDamage;
   [SerializeField] Animator animator;
   [SerializeField] AudioSource hitSFX;
   [SerializeField] AudioSource specialHitSFX;
   private EnemyHealth enemyHealth;
   private bool playerTurn;

   private void Awake()
   {
      enemyHealth = FindObjectOfType<EnemyHealth>();
   }
   
   public void DoDamage()
   {
      enemyHealth.TakeDamage(_attackDamage);
      if (enemyHealth.currentHealth > 0)
      {
         animator.SetTrigger("Hit_Small");
         hitSFX.Play();
      }
      
      Debug.Log("[PlayerAttack] Damage used! " + _attackDamage);
   }

   public void SpecialAttack()
   {
         enemyHealth.TakeDamage(_specialAttackDamage);
         if (enemyHealth.currentHealth > 0)
         {
            animator.SetTrigger("Hit_Big");
            specialHitSFX.Play();
         }
         Debug.Log("[PlayerAttack] Special used! -{_specialAttackDamage}");
   }
   
}
