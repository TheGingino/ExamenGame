using UnityEngine;
using TMPro;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI resultText;

    private bool gameEnded = false;

    public void Win()
    {
        if (gameEnded) return;
        gameEnded = true;

        endScreen.SetActive(true);
        resultText.text = "A";

        Debug.Log("PLAYER WON");
    }

    public void Lose()
    {
        if (gameEnded) return;
        gameEnded = true;

        endScreen.SetActive(true);
        resultText.text = "B";

        Debug.Log("PLAYER LOST");
    }
}