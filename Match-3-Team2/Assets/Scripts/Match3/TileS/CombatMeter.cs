using UnityEngine;

public class CombatMeter : MonoBehaviour
{
   public static CombatMeter Instance { get; private set; }
   public System.Action<TileType> OnMeterFull;

   [Header("Meter Max Value")] 
   public int healMax = 100;
   public int damageMax = 100;
   public int shieldMax = 100;
   public int specialMax = 100;
   
   private int healCurrent;
   private int damageCurrent;
   private int shieldCurrent;
   private int specialCurrent;

   private void Awake()
   {
      Instance = this;
      Debug.Log("[CombatMeter] Aangemaakt en klaar voor gebruik");
   }
   
   public void Add(TileType type, int amount)
   {
      switch (type)
      {
         case TileType.Heal: AddToMeter(ref healCurrent, healMax, amount, "Heal"); break;
         case TileType.Damage: AddToMeter(ref damageCurrent, damageMax, amount, "Damage"); break;
         case TileType.Shield: AddToMeter(ref shieldCurrent, shieldMax, amount, "Shield"); break;
         case TileType.Special: AddToMeter(ref specialMax, specialMax, amount, "Special"); break;
      }  
   }

   private void AddToMeter(ref int current, int max, int amount, string label)
   {
      current += amount;
      Debug.Log($"[CombatMeter] {label} meter: {current}/{max} (+{amount})");
      if (current >= max)
      {
         Debug.Log($"[CombatMeter] {label} VOL — effect triggered! Reset naar 0");
         //current = 0;
         OnMeterFull?.Invoke(GetTileType(label));
      }
   }
   
   TileType GetTileType(string label)
   {
      return (TileType)System.Enum.Parse(typeof(TileType), label);
   }

   public int HealCurrent => healCurrent;
   public int DamageCurrent => damageCurrent;
   public int ShieldCurrent => shieldCurrent;
   public int SpecialCurrent => specialMax;
}
