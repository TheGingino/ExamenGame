using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static GameEndManager;

public class EndScreenUI : MonoBehaviour
{
    private GameEndManager _gameEndManager;

    [SerializeField] private TextMeshProUGUI[] _buttonText;

    private void Start()
    {
        _gameEndManager = FindObjectOfType<GameEndManager>();
    }

    private void OnEnable()
    {
        OnPrimaryButtonPressed();
    }

    public void OnPrimaryButtonPressed()
    {
        if (_gameEndManager == null) return;

        switch (_gameEndManager.GetGameState())
        {
            case GameState.WIN:
                _buttonText[1].text = "Continue";
                HandleWinAction();
                break;

            case GameState.LOSE:
                HandleLoseAction();
                break;
        }
    }

    private void HandleWinAction()
    {
        Debug.Log("Continue");

        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelector");
    }

    private void HandleLoseAction()
    {
        Debug.Log("Retry Level");

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }


}