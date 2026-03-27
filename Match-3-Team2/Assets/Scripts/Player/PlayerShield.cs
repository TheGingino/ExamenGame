using System;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
   public int shieldAmmount { get; private set; }
   [SerializeField] private int _shieldToAdd;

   private void Start()
   {
      shieldAmmount= 0;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.A))
      {
         GainShield();
      }
   }

   public void GainShield()
  {
     shieldAmmount += _shieldToAdd;
     Debug.Log(shieldAmmount);
  }
  
  public int TakeDamage(int damage)
  {
     int absorbed = Mathf.Min(shieldAmmount, damage);
     shieldAmmount -= absorbed;

     return damage - absorbed; // leftover damage
  }
}
