using UnityEngine;
using System.Collections;

public class CombatMeterBar : MonoBehaviour
{
    [SerializeField] private TileType type;
    [SerializeField] private Transform bar;

    [SerializeField] private float lerpSpeed = 8f;
    [SerializeField] private float fullPauseTime = 1f;

    private float currentFill;
    private bool isFullRoutineRunning;

    private void Update()
    {
        if (isFullRoutineRunning) return;
        float targetFill = GetFillPercentage();
        currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * lerpSpeed);

        ApplyScale();
        if (targetFill >= 1f && !isFullRoutineRunning)
        {
            StartCoroutine(HandleFull());
        }
    }

    private IEnumerator HandleFull()
    {
        isFullRoutineRunning = true;

        // Snap to full just to be sure
        currentFill = 1f;
        ApplyScale();

        yield return new WaitForSeconds(fullPauseTime);

        // Reset visually
        currentFill = 0f;
        ApplyScale();

        isFullRoutineRunning = false;
    }

    private void ApplyScale()
    {
        bar.localScale = new Vector3(1f, currentFill, 1f);
        bar.gameObject.SetActive(currentFill > 0.01f);
    }

    private float GetFillPercentage()
    {
        switch (type)
        {
            case TileType.Heal:
                return (float)CombatMeter.Instance.HealCurrent / CombatMeter.Instance.healMax;

            case TileType.Damage:
                return (float)CombatMeter.Instance.DamageCurrent / CombatMeter.Instance.damageMax;

            case TileType.Shield:
                return (float)CombatMeter.Instance.ShieldCurrent / CombatMeter.Instance.shieldMax;

            case TileType.Special:
                return (float)CombatMeter.Instance.SpecialCurrent / CombatMeter.Instance.specialMax;

            default:
                return 0f;
        }
    }
}