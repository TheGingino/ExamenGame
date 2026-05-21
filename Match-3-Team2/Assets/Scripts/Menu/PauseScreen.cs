using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _gridSystem;
    [SerializeField] private GameObject _fpsText;
    [SerializeField] private Toggle _toggle;

    private bool _showingFPS = false;

    private void Start()
    {
        _fpsText.SetActive(_showingFPS);
    }

    public void ResumeGame()
    {
        _gridSystem.SetActive(true);
    }

    public void PauseGame()
    {
        _gridSystem.SetActive(false);
    }

    // Pause Screen Options
    public void ToggleFPS(bool toggle)
    {
        _showingFPS = toggle;
        if (_toggle.isOn == toggle)
        {
            Debug.Log("The Toggle is true " + _toggle.isOn);
            _showingFPS = true;
            _fpsText.SetActive(_showingFPS);
        }
        else if (_toggle.isOn == !toggle)
        {
            Debug.Log("The Toggle is false " + _toggle.isOn);
            _showingFPS = false;
            _fpsText.SetActive(_showingFPS);
        }
    }
}