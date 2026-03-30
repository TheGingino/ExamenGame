using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private int levelProgress;
    [SerializeField] private string saveKey = "LevelProgress";
    [SerializeField] private Button[] levelButtons;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            levelProgress = PlayerPrefs.GetInt(saveKey, 0);
            Debug.Log($"Loaded level progress: {levelProgress}");
            UpdateLevelButtons();
        }
        
        Application.targetFrameRate = 60;
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = i <= levelProgress;
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

}
