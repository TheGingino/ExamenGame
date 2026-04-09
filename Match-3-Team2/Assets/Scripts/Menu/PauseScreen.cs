using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject gridSystem;

    [SerializeField] private GameObject fpsText;
    
    [SerializeField] private Toggle _toggle;
    private bool showingFPS = false;

    private void Start()
    {
        fpsText.SetActive(showingFPS);
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
        if (_toggle.isOn == toggle)
        {
            Debug.Log("The Toggle is true " + _toggle.isOn);
            showingFPS = true;
            fpsText.SetActive(showingFPS);
        }
        else if (_toggle.isOn == !toggle)
        {
            Debug.Log("The Toggle is false " + _toggle.isOn);
            showingFPS = false;
            fpsText.SetActive(showingFPS);
            //run code here if false
        }
    }
}
