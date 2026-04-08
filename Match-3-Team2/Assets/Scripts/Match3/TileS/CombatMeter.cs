using UnityEngine;

public class CombatMeter : MonoBehaviour
{
   public static CombatMeter Instance { get; private set; }
  
   public System.Action<TileType> OnChargeGained;

   [Header("Meter Max Value")] 
   public int healMax = 100;
   public int damageMax = 100;
   public int shieldMax = 100;
   public int specialMax = 100;

   [Header("Max Charges")] 
   public int healMaxCharges = 5;
   public int damageMaxCharges = 5;
   public int shieldMaxCharges = 5;
   public int SpecialMaxCharges = 1;
      
   private int healCurrent;
   private int damageCurrent;
   private int shieldCurrent;
   private int specialCurrent;
   
   private int healCharges;
   private int damageCharges;
   private int shieldCharges;
   private int specialCharges;
   
   private bool specialLockedOut = false;

   private void Awake()
   {
      Instance = this;
      Debug.Log("[CombatMeter] Aangemaakt en klaar voor gebruik");
   }
   
   public void Add(TileType type, int amount)
   {
      switch (type)
      {
         case TileType.Heal:    AddToMeter(ref healCurrent,    healMax,    ref healCharges,    healMaxCharges,    amount, TileType.Heal);    break;
         case TileType.Damage:  AddToMeter(ref damageCurrent,  damageMax,  ref damageCharges,  damageMaxCharges,  amount, TileType.Damage);  break;
         case TileType.Shield:  AddToMeter(ref shieldCurrent,  shieldMax,  ref shieldCharges,  shieldMaxCharges,  amount, TileType.Shield);  break;
         case TileType.Special: AddToMeter(ref specialCurrent, specialMax, ref specialCharges, SpecialMaxCharges, amount, TileType.Special); break;
      }  
   }

   private void AddToMeter(ref int current, int max, ref int charges, int maxCharges, int amount, TileType type)
   {
      // Special: permanently blocked after being used
      if (type == TileType.Special && specialLockedOut)
      {
         Debug.Log("[CombatMeter] Special is permanently locked out.");
         return;
      }
 
      // Don't fill the meter if already at max charges
      if (charges >= maxCharges)
      {
         Debug.Log($"[CombatMeter] {type} at max charges ({maxCharges}), meter won't fill.");
         return;
      }
 
      current += amount;
      Debug.Log($"[CombatMeter] {type} meter: {current}/{max} (+{amount})");
 
      if (current >= max)
      {
         current = 0;
         charges++;
         Debug.Log($"[CombatMeter] {type} charge gained! Total: {charges}/{maxCharges}");
         OnChargeGained?.Invoke(type);
      }
   }
   
   public bool UseCharge(TileType type)
   {
      switch (type)
      {
         case TileType.Heal:
            if (healCharges <= 0) { Debug.Log("[CombatMeter] No Heal charges."); return false; }
            healCharges--;
            return true;

         case TileType.Damage:
            if (damageCharges <= 0) { Debug.Log("[CombatMeter] No Damage charges."); return false; }
            damageCharges--;
            return true;

         case TileType.Shield:
            if (shieldCharges <= 0) { Debug.Log("[CombatMeter] No Shield charges."); return false; }
            shieldCharges--;
            return true;

         case TileType.Special:
            if (specialLockedOut || specialCharges <= 0) { Debug.Log("[CombatMeter] Special not available."); return false; }
            specialCharges--;
            specialLockedOut = true;
            return true;
      }
      return false;
   }

   public int HealCharges => healCharges;
   public int DamageCharges => damageCharges;
   public int ShieldCharges => shieldCurrent;
   public int SpecialCharges => specialCharges;
   private bool SpecialLockedOut => specialLockedOut;
   
   public int HealCurrent => healCurrent;
   public int DamageCurrent => damageCurrent;
   public int ShieldCurrent => shieldCurrent;
   public int SpecialCurrent => specialMax;

}
