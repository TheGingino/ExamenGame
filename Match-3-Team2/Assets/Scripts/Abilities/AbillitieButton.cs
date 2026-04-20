using UnityEngine;
using UnityEngine.UI;

public class AbillitieButton : MonoBehaviour
{
    
    [SerializeField] private Button button;
    [SerializeField] private TileType abilityType;

    private bool isReady;

    private void Awake()
    {
        button.interactable = false;
    }

    private void OnEnable()
    {
        if (CombatMeter.Instance != null)
        {
            CombatMeter.Instance.OnChargeGained += HandleMeterFull;
            CombatMeter.Instance.OnAbilityLimitChanged += UpdateButtonState;
        }
    }
    
    void Start()
    {
        if (CombatMeter.Instance != null)
        {
            CombatMeter.Instance.OnChargeGained += HandleMeterFull;
            CombatMeter.Instance.OnAbilityLimitChanged += UpdateButtonState;
        }
        UpdateButtonState();
    }

    private void OnDisable()
    {
        CombatMeter.Instance.OnChargeGained -= HandleMeterFull;
        CombatMeter.Instance.OnAbilityLimitChanged -= UpdateButtonState;
    }

    void HandleMeterFull(TileType type)
    {
        Debug.Log("[AbilityButton-{abilityType}] EVENT RECEIVED: {type}");

        if (type != abilityType) return;

        Debug.Log("[AbilityButton-{abilityType}] Charge gained");

        UpdateButtonState();
    }

    public void OnClick()
    {
        Debug.Log("[AbilityButton-{abilityType}] CLICKED");
        if (!CombatMeter.Instance.CanUseAbility()) return;
       
        if (!CombatMeter.Instance.UseCharge(abilityType))
        {
            Debug.Log("[AbilityButton-{abilityType}] No charges available");
            return;
        }

        Debug.Log("[AbilityButton-{abilityType}] USED");

        ExecuteAbility();

        // Update interactable state AFTER using charge
        UpdateButtonState();
    }
    
    void UpdateButtonState()
    {
        int charges = 0;

        switch (abilityType)
        {
            case TileType.Heal: charges = CombatMeter.Instance.HealCharges; break;
            case TileType.Damage: charges = CombatMeter.Instance.DamageCharges; break;
            case TileType.Shield: charges = CombatMeter.Instance.ShieldCharges; break;
            case TileType.Special: charges = CombatMeter.Instance.SpecialCharges; break;
        }

        button.interactable = charges > 0 && CombatMeter.Instance.CanUseAbility();
    }

    void ExecuteAbility()
    {
        switch (abilityType)
        {
            case TileType.Damage:
                FindObjectOfType<PlayerAttack>().DoDamage();
                break;

            case TileType.Heal:
                FindObjectOfType<PlayerHealth>().Heal();
                break;

            case TileType.Shield:
                FindObjectOfType<PlayerShield>().GainShield();
                break;

            case TileType.Special:
                FindObjectOfType<PlayerAttack>().SpecialAttack();
                break;
        }
    }
    
}
