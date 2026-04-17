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

    private SwapTiles swapTiles;

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
        UpdateSwapUI();
    }

    private void EndPlayerTurn()
    {
        playerTurn = false;
        swapTiles.SetInputState(false);
        CombatMeter.Instance.ResetAbiltyUses();
        StartCoroutine(EnemyTurnCoroutine());
    }

    private IEnumerator EnemyTurnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        OnEnemyTurn?.Invoke();

        yield return new WaitForSeconds(1f);

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
}

