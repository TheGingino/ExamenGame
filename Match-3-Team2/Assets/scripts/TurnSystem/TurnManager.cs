using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private int _maxSwapsPerTurn = 5;

    public bool playerTurn = true;
    private int _currentSwaps;

    [Header("Enemy Turn Event")]
    public UnityEvent OnEnemyTurn;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _swapCounterText;
    [SerializeField] private GameObject _playerTurnIndicator;
    [SerializeField] private GameObject _enemyTurnIndicator;
    [SerializeField] private float _fadeDuration = 0.5f;

    public UnityEvent OnPlayerTurnStart;
    public UnityEvent OnEnemyTurnStart;

    private SwapTiles _swapTiles;
    private Coroutine _fadeCoroutine;

    private void Start()
    {
        _swapTiles = FindObjectOfType<SwapTiles>();
        StartPlayerTurn();
    }

    public void RegisterSwap()
    {
        if (!playerTurn) return;

        _currentSwaps++;

        UpdateSwapUI();

        if (_currentSwaps >= _maxSwapsPerTurn)
        {
            EndPlayerTurn();
        }
    }

    private void StartPlayerTurn()
    {
        playerTurn = true;
        _currentSwaps = 0;
        _swapTiles.SetInputState(true);

        OnPlayerTurnStart?.Invoke();
        ShowTurnImage(true); // Show player turn indicator

        UpdateSwapUI();
    }

    private void EndPlayerTurn()
    {
        playerTurn = false;
        _swapTiles.SetInputState(false);

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
        if (_swapCounterText != null)
        {
            int remaining = _maxSwapsPerTurn - _currentSwaps;
            _swapCounterText.text = remaining.ToString();
        }
    }
    
    public void ShowTurnImage(bool isPlayer)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(FadeTurnIndicator(isPlayer));
    }

    private IEnumerator FadeTurnIndicator(bool isPlayer)
    {
        GameObject currentIndicator = isPlayer ? _enemyTurnIndicator : _playerTurnIndicator;
        GameObject nextIndicator = isPlayer ? _playerTurnIndicator : _enemyTurnIndicator;

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
    
        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / _fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    
        if (targetAlpha == 0f)
            indicator.SetActive(false);
    }
}

