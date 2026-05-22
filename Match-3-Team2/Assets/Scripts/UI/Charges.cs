using UnityEngine;

public class Charges : MonoBehaviour
{
    [SerializeField] private GameObject[] _chargeIcons;
    [SerializeField] private TileType _type;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        int charges = GetCharges();

        for (int i = 0; i < _chargeIcons.Length; i++)
        {
            _chargeIcons[i].SetActive(i < charges);
        }
    }

    private int GetCharges()
    {
        switch (_type)
        {
            case TileType.Heal: return CombatMeter.Instance.HealCharges;
            case TileType.Damage: return CombatMeter.Instance.DamageCharges;
            case TileType.Shield: return CombatMeter.Instance.ShieldCharges;
            case TileType.Special: return CombatMeter.Instance.SpecialCharges;
            default: return 0;
        }
    }
}