using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [SerializeField] private Image damageRim;
    [SerializeField] private float fadeDuration = 2f;

    private Coroutine fadeRoutine;

    private void Start()
    {
        SetAlpha(0f);
    }

    public void ShowDamage()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        SetAlpha(1f);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        Color color = damageRim.color;
        color.a = alpha;
        damageRim.color = color;
    }
}
