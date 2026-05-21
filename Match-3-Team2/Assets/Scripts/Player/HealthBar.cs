using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider _slider;
    [SerializeField] private float _smoothSpeed = 5f;

    private float _targetValue;

    [SerializeField] private Image _healthBarImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _shieldSprite;

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
        _targetValue = health;
    }

    public void SetHealth(int health)
    {
        _targetValue = health;
    }

    private void Update()
    {
        _slider.value = Mathf.Lerp(_slider.value, _targetValue, Time.deltaTime * _smoothSpeed);

        if (Mathf.Abs(_slider.value - _targetValue) < 0.1f)
        {
            _slider.value = _targetValue;
        }
    }
    public void ShowShieldVisual()
    {
         _healthBarImage.sprite = _shieldSprite;
    }
    public void HideShieldVisual()
    {
        _healthBarImage.sprite = _shieldSprite;
    }
}