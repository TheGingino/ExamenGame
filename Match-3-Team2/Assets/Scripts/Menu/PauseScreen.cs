using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject gridSystem;

    [SerializeField] private GameObject fpsText;
    private bool showingFPS = false;

    private void Start()
    {
        fpsText.SetActive(false);
    }

    public void ResumeGame()
    {
        gridSystem.SetActive(true);
    }

    public void PauseGame()
    {
        gridSystem.SetActive(false);
    }
    
    //Pause Screen Options
    public void ToggleFPS(bool toggle)
    {
        showingFPS = toggle;
        fpsText.SetActive(showingFPS);
    }
}
