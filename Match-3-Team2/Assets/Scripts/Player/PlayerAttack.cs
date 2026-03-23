using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] int _attackDamage;

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
         Debug.Log("DAMANGE");
      }
   }

   void DoDamage()
   {
      enemyHealth.TakeDamage(_attackDamage);
   }
   
}
