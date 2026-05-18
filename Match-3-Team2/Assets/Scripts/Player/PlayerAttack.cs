using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] int _attackDamage;
   [SerializeField] int _specialAttackDamage;
   [SerializeField] Animator _hitAnimator;
   [SerializeField] Animator _slahsAnimator;
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
      if (enemyHealth._currentHealth > 0)
      {
         _hitAnimator.SetTrigger("Hit_Small");
         _slahsAnimator.SetTrigger("Slash");
         hitSFX.Play();
      }
      
      Debug.Log("[PlayerAttack] Damage used! " + _attackDamage);
   }

   public void SpecialAttack()
   {
         enemyHealth.TakeDamage(_specialAttackDamage);
         if (enemyHealth._currentHealth > 0)
         {
            _hitAnimator.SetTrigger("Hit_Big");
            _slahsAnimator.SetTrigger("Slash");
            specialHitSFX.Play();
         }
         Debug.Log("[PlayerAttack] Special used! -{_specialAttackDamage}");
   }
   
}
