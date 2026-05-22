using UnityEngine;

public class CombatMeter : MonoBehaviour
{
    public static CombatMeter Instance { get; private set; }

    public System.Action<TileType> OnChargeGained;
    public System.Action OnAbilityLimitChanged;

    [Header("Meter Max Value")]
    public int healMax = 100;
    public int damageMax = 100;
    public int shieldMax = 100;
    public int specialMax = 100;

    [Header("Max Charges")]
    public int healMaxCharges = 5;
    public int damageMaxCharges = 5;
    public int shieldMaxCharges = 5;
    public int specialMaxCharges = 1;

    [Header("Turn Limit")]
    public int maxAbilitiesPerTurn = 3;

    private int _healCurrent;
    private int _damageCurrent;
    private int _shieldCurrent;
    private int _specialCurrent;

    private int _healCharges;
    private int _damageCharges;
    private int _shieldCharges;
    private int _specialCharges;

    private int _abilitiesUsedThisTurn;

    private void Awake()
    {
        Instance = this;
        Debug.Log("[CombatMeter] Aangemaakt en klaar voor gebruik");
    }

    public bool CanUseAbility() => _abilitiesUsedThisTurn < maxAbilitiesPerTurn;
    public int AbilitiesRemaining => maxAbilitiesPerTurn - _abilitiesUsedThisTurn;

    public void ResetAbilityUses()
    {
        _abilitiesUsedThisTurn = 0;
        OnAbilityLimitChanged?.Invoke();
    }

    public void Add(TileType type, int amount)
    {
        switch (type)
        {
            case TileType.Heal: AddToMeter(ref _healCurrent, healMax, ref _healCharges, healMaxCharges, amount, TileType.Heal); break;
            case TileType.Damage: AddToMeter(ref _damageCurrent, damageMax, ref _damageCharges, damageMaxCharges, amount, TileType.Damage); break;
            case TileType.Shield: AddToMeter(ref _shieldCurrent, shieldMax, ref _shieldCharges, shieldMaxCharges, amount, TileType.Shield); break;
            case TileType.Special: AddToMeter(ref _specialCurrent, specialMax, ref _specialCharges, specialMaxCharges, amount, TileType.Special); break;
        }
    }

    private void AddToMeter(ref int current, int max, ref int charges, int maxCharges, int amount, TileType type)
    {
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
        if (!CanUseAbility())
        {
            Debug.Log("[CombatMeter] Ability limit reached this turn.");
            return false;
        }

        switch (type)
        {
            case TileType.Heal:
                if (_healCharges <= 0) { Debug.Log("[CombatMeter] No Heal charges."); return false; }
                _healCharges--;
                break;

            case TileType.Damage:
                if (_damageCharges <= 0) { Debug.Log("[CombatMeter] No Damage charges."); return false; }
                _damageCharges--;
                break;

            case TileType.Shield:
                if (_shieldCharges <= 0) { Debug.Log("[CombatMeter] No Shield charges."); return false; }
                _shieldCharges--;
                break;

            case TileType.Special:
                if (_specialCharges <= 0) { Debug.Log("[CombatMeter] Special not available."); return false; }
                _specialCharges--;
                break;

            default:
                return false;
        }

        _abilitiesUsedThisTurn++;
        OnAbilityLimitChanged?.Invoke();
        return true;
    }

    public int HealCharges => _healCharges;
    public int DamageCharges => _damageCharges;
    public int ShieldCharges => _shieldCharges;
    public int SpecialCharges => _specialCharges;

    public int HealCurrent => _healCurrent;
    public int DamageCurrent => _damageCurrent;
    public int ShieldCurrent => _shieldCurrent;
    public int SpecialCurrent => _specialCurrent;
}