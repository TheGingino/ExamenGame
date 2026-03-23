using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private float smoothSpeed = 5f;

    private float targetValue;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        targetValue = health;
    }

    public void SetHealth(int health)
    {
        targetValue = health;
    }

    private void Update()
    {
        slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * smoothSpeed);
    }
}