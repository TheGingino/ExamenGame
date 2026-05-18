using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public int maxSwapsPerTurn = 5;
    private int currentSwaps;

    public bool playerTurn = true;

    [Header("Enemy Turn Event")]
    public UnityEvent OnEnemyTurn;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI swapCounterText;
    [SerializeField] private GameObject playerTurnIndicator;
    [SerializeField] private GameObject enemyTurnIndicator;
    [SerializeField] private float fadeDuration = 0.5f;

    
    public UnityEvent OnPlayerTurnStart;
    public UnityEvent OnEnemyTurnStart;

    private SwapTiles swapTiles;
    private Coroutine fadeCoroutine;

    private void Start()
    {
        swapTiles = FindObjectOfType<SwapTiles>();
        StartPlayerTurn();
    }

    public void RegisterSwap()
    {
        if (!playerTurn) return;

        currentSwaps++;

        UpdateSwapUI();

        if (currentSwaps >= maxSwapsPerTurn)
        {
            EndPlayerTurn();
        }
    }

    private void StartPlayerTurn()
    {
        playerTurn = true;
        currentSwaps = 0;
        swapTiles.SetInputState(true);

        OnPlayerTurnStart?.Invoke();
        ShowTurnImage(true); // Show player turn indicator

        UpdateSwapUI();
    }

    private void EndPlayerTurn()
    {
        playerTurn = false;
        swapTiles.SetInputState(false);

        OnEnemyTurnStart?.Invoke();
        ShowTurnImage(false); // Show enemy turn indicator

        StartCoroutine(EnemyTurnCoroutine());
    }
    private IEnumerator EnemyTurnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        OnEnemyTurn?.Invoke();

        yield return new WaitForSeconds(2.5f);

        StartPlayerTurn(); // This resets swaps + UI
    }

    private void UpdateSwapUI()
    {
        if (swapCounterText != null)
        {
            int remaining = maxSwapsPerTurn - currentSwaps;
            swapCounterText.text = remaining.ToString();
        }
    }
    
    public void ShowTurnImage(bool isPlayer)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeTurnIndicator(isPlayer));
    }

    private IEnumerator FadeTurnIndicator(bool isPlayer)
    {
        GameObject currentIndicator = isPlayer ? enemyTurnIndicator : playerTurnIndicator;
        GameObject nextIndicator = isPlayer ? playerTurnIndicator : enemyTurnIndicator;

        if (currentIndicator != null && currentIndicator.activeInHierarchy)
        {
            yield return StartCoroutine(Fade(currentIndicator, 0f));
        }

        if (nextIndicator != null)
        {
            nextIndicator.SetActive(true);
            yield return StartCoroutine(Fade(nextIndicator, 1f));
            yield return StartCoroutine(Fade(nextIndicator, 0f));
        }
    }
    
    private IEnumerator Fade(GameObject indicator, float targetAlpha)
    {
        CanvasGroup canvasGroup = indicator.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = indicator.AddComponent<CanvasGroup>();

        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;
    
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    
        if (targetAlpha == 0f)
            indicator.SetActive(false);
    }
}

