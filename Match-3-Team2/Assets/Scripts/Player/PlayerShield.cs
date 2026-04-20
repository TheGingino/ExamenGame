using System;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
   public int shieldAmmount { get; private set; }
   [SerializeField] private int _shieldToAdd;
   [SerializeField] private AudioSource shieldSFX;

   private void Start()
   {
      shieldAmmount= 0;
   }
   public void GainShield()
  {
        shieldAmmount += _shieldToAdd;
        shieldSFX.Play();
        Debug.Log("[PlayerShield] Shield: {shieldAmmount}");
  }
  
  public int TakeDamage(int damage)
  {
     int absorbed = Mathf.Min(shieldAmmount, damage);
     shieldAmmount -= absorbed;

     return damage - absorbed; // leftover damage
  }
}
