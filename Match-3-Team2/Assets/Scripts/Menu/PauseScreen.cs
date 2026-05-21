using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _fpsText;
    [SerializeField] private Toggle _toggle;

    private bool _showingFPS = false;

    private void Start()
    {
        _fpsText.SetActive(_showingFPS);
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