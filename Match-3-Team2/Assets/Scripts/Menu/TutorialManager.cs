using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _pages;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _prevButton;
    [SerializeField] private GameObject _mainPage;
    [SerializeField] private TMP_Text _nextButtonText;

    private int _currentPage = 0;

    private void Awake()
    {
        ShowPage(0);
    }

    public void NextPage()
    {
        if (_currentPage < _pages.Length - 1)
        {
            _currentPage++;
            ShowPage(_currentPage);
        }
        else
        {
            EndTutorial();
        }
    }

    public void PrevPage()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            ShowPage(_currentPage);
        }
    }

    public void SkipTutorial()
    {
        EndTutorial();
    }

    private void ShowPage(int index)
    {
        _mainPage.SetActive(true);

        for (int i = 0; i < _pages.Length; i++)
        {
            _pages[i].SetActive(i == index);
        }

        if (_nextButtonText != null)
        {
            _nextButtonText.text = (index == _pages.Length - 1) ? "Start" : "Next";
        }
    }

    private void EndTutorial()
    {
        gameObject.SetActive(false);
    }
}