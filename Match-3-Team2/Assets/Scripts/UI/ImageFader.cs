using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [SerializeField] private Image _damageRim;
    [SerializeField] private float _fadeDuration = 2f;

    private Coroutine _fadeRoutine;

    private void Start()
    {
        SetAlpha(0f);
    }

    public void ShowDamage()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        SetAlpha(1f);

        float timer = 0f;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, timer / _fadeDuration);
            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        Color color = _damageRim.color;
        color.a = alpha;
        _damageRim.color = color;
    }
}
