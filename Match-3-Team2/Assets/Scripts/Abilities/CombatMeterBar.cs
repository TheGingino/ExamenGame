using UnityEngine;
using System.Collections;

public class CombatMeterBar : MonoBehaviour
{
    [SerializeField] private TileType _type;
    [SerializeField] private Transform _bar;

    [SerializeField] private float _lerpSpeed = 8f;
    [SerializeField] private float _fullPauseTime = 1f;

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _chargeSFX;

    private float _currentFill;
    private bool _isFullRoutineRunning;

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
        if (_isFullRoutineRunning)
            return;

        float targetFill = GetFillPercentage();

        _currentFill = Mathf.Lerp(
            _currentFill,
            targetFill,
            Time.deltaTime * _lerpSpeed
        );

        _currentFill = Mathf.Clamp01(_currentFill);

        ApplyScale();
    }

    private void HandleChargeGained(TileType gainedType)
    {
        if (gainedType != _type)
            return;

        StartCoroutine(HandleFull());
    }

    private IEnumerator HandleFull()
    {
        _isFullRoutineRunning = true;

        _currentFill = 1f;
        ApplyScale();

        _animator.SetTrigger("Surge");
        _chargeSFX.Play();
        yield return new WaitForSeconds(_fullPauseTime);

        _currentFill = 0f;

        ApplyScale();

        _isFullRoutineRunning = false;
    }

    private void ApplyScale()
    {
        _bar.localScale = new Vector3(
            1f,
            _currentFill,
            1f
        );

        _bar.gameObject.SetActive(_currentFill > 0.01f);
    }

    private float GetFillPercentage()
    {
        switch (_type)
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