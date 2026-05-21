using UnityEngine;
using UnityEngine.UI;

public class AbillitieButton : MonoBehaviour
{
    private TurnManager _turnManager;
    [SerializeField] private Button _button;
    [SerializeField] private TileType _abilityType;

    private bool _isReady;

    private void Awake()
    {
        _button.interactable = false;
        _turnManager = FindObjectOfType<TurnManager>();
    }

    private void OnEnable()
    {
        if (CombatMeter.Instance != null)
        {
            CombatMeter.Instance.OnChargeGained += HandleMeterFull;
            CombatMeter.Instance.OnAbilityLimitChanged += UpdateButtonState;
        }
        _turnManager.OnPlayerTurnStart.AddListener(UpdateButtonState);
        _turnManager.OnEnemyTurnStart.AddListener(UpdateButtonState);
    }

    private void Start()
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

    private void HandleMeterFull(TileType type)
    {
        Debug.Log("[AbilityButton-{_abilityType}] EVENT RECEIVED: {type}");

        if (type != _abilityType) return;

        Debug.Log("[AbilityButton-{_abilityType}] Charge gained");

        UpdateButtonState();
    }

    public void OnClick()
    {
        if (!_turnManager.playerTurn)
        {
            Debug.Log("Not player turn");
            return;
        }
        Debug.Log($"[AbilityButton-{_abilityType}] CLICKED");

        if (!CombatMeter.Instance.UseCharge(_abilityType))
        {
            Debug.Log("[AbilityButton-{_abilityType}] No charges available");
            return;
        }

        Debug.Log("[AbilityButton-{_abilityType}] USED");

        ExecuteAbility();

        // Update interactable state AFTER using charge
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        int charges = 0;

        switch (_abilityType)
        {
            case TileType.Heal: charges = CombatMeter.Instance.HealCharges; break;
            case TileType.Damage: charges = CombatMeter.Instance.DamageCharges; break;
            case TileType.Shield: charges = CombatMeter.Instance.ShieldCharges; break;
            case TileType.Special: charges = CombatMeter.Instance.SpecialCharges; break;
        }

        _button.interactable = charges > 0 && _turnManager.playerTurn;
        _button.interactable = charges > 0 && CombatMeter.Instance.CanUseAbility() && _turnManager.playerTurn;
    }

    private void ExecuteAbility()
    {
        switch (_abilityType)
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