using System;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
   public int shieldAmmount { get; private set; }
   private int shieldToAdd;

   private void Start()
   {
      shieldAmmount= 0;
   }

  private void GainShield()
  {
     shieldAmmount += shieldToAdd;
  }
  
  public int TakeDamage(int damage)
  {
     int absorbed = Mathf.Min(shieldAmmount, damage);
     shieldAmmount -= absorbed;

     return damage - absorbed; // leftover damage
  }
}
