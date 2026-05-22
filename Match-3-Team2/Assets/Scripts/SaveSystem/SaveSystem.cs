using System;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private int _levelProgress;
    [SerializeField] private string _saveKey = "LevelProgress";
    [SerializeField] private CubeButton[] _cubeLevelButtons;

    private void Start()
    {
        if (PlayerPrefs.HasKey(_saveKey))
        {
            _levelProgress = PlayerPrefs.GetInt(_saveKey, 0);
            Debug.Log($"Loaded level progress: {_levelProgress}");
            UpdateLevelButtons();
        }

        Application.targetFrameRate = 60;
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < _cubeLevelButtons.Length; i++)
        {
            bool isUnlocked = i <= _levelProgress;
            _cubeLevelButtons[i].SetInteractable(isUnlocked);
        }
    }

    public void UpdateLevelProgress()
    {
        _levelProgress++;
        PlayerPrefs.SetInt(_saveKey, _levelProgress);
        UpdateLevelButtons();
    }

    public void ResetProgress()
    {
        _levelProgress = 0;
        PlayerPrefs.SetInt(_saveKey, _levelProgress);
        UpdateLevelButtons();
    }
    
    public void TestCompleteFirstLevel()
    {
        UpdateLevelProgress();
        Debug.Log($"Level progress updated to: {_levelProgress}");
    }
}