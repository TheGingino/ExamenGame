using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject _mainPage;

    private int currentPage = 0;

    [SerializeField] private Text nextButtonText;

    private void Awake()
    {
        ShowPage(0);
        
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
        else
        {
            EndTutorial();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

    public void SkipTutorial()
    {
        EndTutorial();
    }

    private void ShowPage(int index)
    {
        _mainPage.SetActive(true);

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }

        if (nextButtonText != null)
        {
            nextButtonText.text = (index == pages.Length - 1) ? "Start" : "Next";
        }
    }

    private void EndTutorial()
    {
        gameObject.SetActive(false);
    }
}