using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _specialAttackDamage;
    [SerializeField] private Animator _hitAnimator;
    [SerializeField] private Animator _slashAnimator;
    [SerializeField] private Animator _specialAttackAnimator;
    [SerializeField] private AudioSource _hitSFX;
    [SerializeField] private AudioSource _specialHitSFX;

    private EnemyHealth _enemyHealth;
    private bool _playerTurn;

    private void Awake()
   {
      
        _enemyHealth = FindObjectOfType<EnemyHealth>();
   }
   
   public void DoDamage()
   {
      _enemyHealth.TakeDamage(_attackDamage);
      if (_enemyHealth._currentHealth > 0)
      {
         _hitAnimator.SetTrigger("Hit_Small");
         _slashAnimator.SetTrigger("Slash");
         _hitSFX.Play();
      }
      
      Debug.Log("[PlayerAttack] Damage used! " + _attackDamage);
   }

   public void SpecialAttack()
   {
         _enemyHealth.TakeDamage(_specialAttackDamage);
         if (_enemyHealth._currentHealth > 0)
         {
            _hitAnimator.SetTrigger("Hit_Big");
            _specialAttackAnimator.SetTrigger("special attack");
            _specialHitSFX.Play();
         }
         Debug.Log("[PlayerAttack] Special used! -{_specialAttackDamage}");
   }
   
}
