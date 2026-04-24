using System;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private int levelProgress;
    [SerializeField] private string saveKey = "LevelProgress";
    [SerializeField] private CubeButton[] cubeLevelButtons;

    private void Start()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            levelProgress = PlayerPrefs.GetInt(saveKey, 0);
            Debug.Log($"Loaded level progress: {levelProgress}");
            UpdateLevelButtons();
        }

        Application.targetFrameRate = 60;
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < cubeLevelButtons.Length; i++)
        {
            bool isUnlocked = i <= levelProgress;
            cubeLevelButtons[i].SetInteractable(isUnlocked);
        }
    }

    public void UpdateLevelProgress()
    {
        levelProgress++;
        PlayerPrefs.SetInt(saveKey, levelProgress);
        UpdateLevelButtons();
    }

    public void ResetProgress()
    {
        levelProgress = 0;
        PlayerPrefs.SetInt(saveKey, levelProgress);
        UpdateLevelButtons();
    }
    
    public void TestCompleteFirstLevel()
    {
        UpdateLevelProgress();
        Debug.Log($"Level progress updated to: {levelProgress}");
    }
}