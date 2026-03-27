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
            CombatMeter.Instance.OnMeterFull += HandleMeterFull;
        }
    }

    private void OnDisable()
    {
        CombatMeter.Instance.OnMeterFull -= HandleMeterFull;
    }

    void HandleMeterFull(TileType type)
    {
        // Only react if it's THIS button's ability
        if (type != abilityType) return;

        Debug.Log($"[AbilityButton-{abilityType}] READY");

        isReady = true;
        button.interactable = true;
    }

    public void OnClick()
    {
        if (!isReady) return;

        Debug.Log($"[AbilityButton-{abilityType}] USED");

        ExecuteAbility();

        isReady = false;
        button.interactable = false;
    }

    void ExecuteAbility()
    {
        switch (abilityType)
        {
            case TileType.Damage:
                FindObjectOfType<EnemyHealth>().TakeDamage(5);
                break;

            case TileType.Heal:
                FindObjectOfType<PlayerHealth>().GainHealth();
                break;

            case TileType.Shield:
                FindObjectOfType<PlayerShield>().GainShield();
                break;

            case TileType.Special:
                Debug.Log("Special not implemented");
                break;
        }
    }
    
}
