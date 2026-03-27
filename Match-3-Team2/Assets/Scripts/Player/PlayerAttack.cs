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
      if (CombatMeter.Instance.UseCharge(TileType.Damage))
      {
         enemyHealth.TakeDamage(_attackDamage);
         Debug.Log("[PlayerAttack] Damage used! -{_attackDamage}");
      }
   }


   void SpecialAttack()
   {
      if (CombatMeter.Instance.UseCharge(TileType.Special))
      {
         enemyHealth.TakeDamage(_specialAttackDamage);
         Debug.Log("[PlayerAttack] Special used! -{_specialAttackDamage}");
      }
   }
   
}
