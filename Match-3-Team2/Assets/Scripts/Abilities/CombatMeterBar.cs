using UnityEngine;
using System.Collections;

public class CombatMeterBar : MonoBehaviour
{
    [SerializeField] private TileType type;
    [SerializeField] private Transform bar;

    [SerializeField] private float lerpSpeed = 8f;
    [SerializeField] private float fullPauseTime = 1f;

    [SerializeField] private Animator _animator;

    private float currentFill;
    private bool isFullRoutineRunning;

    private void Start()
    {
        if (CombatMeter.Instance != null)
        {
            CombatMeter.Instance.OnChargeGained += HandleChargeGained;
        }
    }

    private void OnDestroy()
    {
        if (CombatMeter.Instance != null)
        {
            CombatMeter.Instance.OnChargeGained -= HandleChargeGained;
        }
    }

    private void Update()
    {
        if (isFullRoutineRunning)
            return;

        float targetFill = GetFillPercentage();

        currentFill = Mathf.Lerp(
            currentFill,
            targetFill,
            Time.deltaTime * lerpSpeed
        );

        currentFill = Mathf.Clamp01(currentFill);

        ApplyScale();
    }

    private void HandleChargeGained(TileType gainedType)
    {
        if (gainedType != type)
            return;

        StartCoroutine(HandleFull());
    }

    private IEnumerator HandleFull()
    {
        isFullRoutineRunning = true;

        currentFill = 1f;
        ApplyScale();
        
        _animator.SetTrigger("Surge");
        yield return new WaitForSeconds(fullPauseTime);

        currentFill = 0f;

        ApplyScale();

        isFullRoutineRunning = false;
    }

    private void ApplyScale()
    {
        bar.localScale = new Vector3(
            1f,
            currentFill,
            1f
        );

        bar.gameObject.SetActive(currentFill > 0.01f);
    }

    private float GetFillPercentage()
    {
        switch (type)
        {
            case TileType.Heal:
                return (float)CombatMeter.Instance.HealCurrent /
                       CombatMeter.Instance.healMax;

            case TileType.Damage:
                return (float)CombatMeter.Instance.DamageCurrent /
                       CombatMeter.Instance.damageMax;

            case TileType.Shield:
                return (float)CombatMeter.Instance.ShieldCurrent /
                       CombatMeter.Instance.shieldMax;

            case TileType.Special:
                return (float)CombatMeter.Instance.SpecialCurrent /
                       CombatMeter.Instance.specialMax;

            default:
                return 0f;
        }
    }
}