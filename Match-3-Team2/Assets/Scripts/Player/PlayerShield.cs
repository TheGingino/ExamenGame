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
      Debug.Log($"[PlayerShield] Shield gained. Current shield: {shieldAmmount}");
   }

   public int TakeDamage(int damage)
   {
      int absorbed = Mathf.Min(shieldAmmount, damage);
      shieldAmmount -= absorbed;

      Debug.Log($"[PlayerShield] Took damage. Absorbed: {absorbed}, Remaining shield: {shieldAmmount}");

      return damage - absorbed;
   }
}
