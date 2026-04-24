using UnityEngine;

public class Charges : MonoBehaviour
{
    [SerializeField] private GameObject[] chargeIcons;
    [SerializeField] private TileType type;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        int charges = GetCharges();

        for (int i = 0; i < chargeIcons.Length; i++)
        {
            chargeIcons[i].SetActive(i < charges);
        }
    }

    private int GetCharges()
    {
        switch (type)
        {
            case TileType.Heal: return CombatMeter.Instance.HealCharges;
            case TileType.Damage: return CombatMeter.Instance.DamageCharges;
            case TileType.Shield: return CombatMeter.Instance.ShieldCharges;
            case TileType.Special: return CombatMeter.Instance.SpecialCharges;
            default: return 0;
        }
    }
}