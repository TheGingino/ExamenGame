using UnityEngine;

public class CombatMeter : MonoBehaviour
{
   public static CombatMeter Instance { get; private set; }

   [Header("Meter Max Value")] 
   public int healMax = 100;
   public int damageMax = 100;
   public int shieldMax = 100;
   public int specialMax = 100;
   
   private int _healCurrent;
   private int _damageCurrent;
   private int _shieldCurrent;
   private int _specialCurrent;

   private void Awake()
   {
      Instance = this;
      Debug.Log("[CombatMeter] Aangemaakt en klaar voor gebruik");
   }
   
   public void Add(TileType type, int amount)
   {
      switch (type)
      {
         case TileType.Heal: AddToMeter(ref _healCurrent, healMax, amount, "Heal"); break;
         case TileType.Damage: AddToMeter(ref _damageCurrent, damageMax, amount, "Damage"); break;
         case TileType.Shield: AddToMeter(ref _shieldCurrent, shieldMax, amount, "Shield"); break;
         case TileType.Special: AddToMeter(ref _specialCurrent, specialMax, amount, "Special"); break;
      }  
   }

   private void AddToMeter(ref int current, int max, int amount, string label)
   {
      current += amount;
      Debug.Log($"[CombatMeter] {label} meter: {current}/{max} (+{amount})");
      if (current >= max)
      {
         Debug.Log($"[CombatMeter] {label} VOL — effect triggered! Reset naar 0");
         current = 0;
         // TODO: trigger effect once PlayerStats / EnemyStats exist
         // PlayerStats.Instance.Heal(healMax);
      }
   }

   public int HealCurrent => _healCurrent;
   public int DamageCurrent => _damageCurrent;
   public int ShieldCurrent => _shieldCurrent;
   public int SpecialCurrent => _specialCurrent;
}
