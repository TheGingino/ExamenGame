using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameEndManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private AudioSource _winSFX;
    [SerializeField] private AudioSource _loseSFX;

    [SerializeField] private TextMeshProUGUI[] _buttonText;

    [Header("Events")]
    [SerializeField] private UnityEvent _onWin;
    [SerializeField] private UnityEvent _onLose;

    public enum GameState
    {
        PLAYING,
        WIN,
        LOSE
    }

    private GameState _currentState = GameState.PLAYING;

    private void Start()
    {
        Time.timeScale = 1f;
        _endScreen.SetActive(false);
    }

    public bool IsGameActive()
    {
        return _currentState == GameState.PLAYING;
    }

    public void TriggerWin()
    {
        if (!IsGameActive()) return;
        _winSFX.Play();
        _currentState = GameState.WIN;
        HandleEndState();
    }

    public void TriggerLose()
    {
        if (!IsGameActive()) return;
        _loseSFX.Play();
        _currentState = GameState.LOSE;
        HandleEndState();
    }

    private void HandleEndState()
    {
        DisableGameplay();

        _endScreen.SetActive(true);

        switch (_currentState)
        {
            case GameState.WIN:
                _buttonText[0].text = "You Win!";
                _onWin?.Invoke();
                _buttonText[1].text = "Continue";
                break;

            case GameState.LOSE:
                _buttonText[0].text = "You Lost";
                _onLose?.Invoke();
                _buttonText[1].text = "Retry";
                break;
        }
    }

    private void DisableGameplay()
    {
        Time.timeScale = 0f;

        SwapTiles swapTiles = FindObjectOfType<SwapTiles>();
        if (swapTiles != null)
        {
            swapTiles.SetInputState(false);
        }
    }

    public GameState GetGameState()
    {
        return _currentState;
    }
}