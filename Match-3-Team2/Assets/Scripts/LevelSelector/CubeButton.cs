using System;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] private string _buttonID;
    [SerializeField] private LevelSelector _levelSelector;
    [SerializeField] private Material _lockedMaterial;
    [SerializeField] private Material _unlockedMaterial;

    private bool _isInteractable;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _isInteractable = true;
        if (_levelSelector == null)
        {
            Debug.LogError($"CubeButton '{name}' has no LevelSelector assigned.");
        }
    }

    private void OnMouseDown()
    {
        if (_isInteractable && _levelSelector != null)
        {
            _levelSelector.LevelSelect();

            if (_buttonID == "lvl1")
            {
                SaveSystem saveSystem = FindObjectOfType<SaveSystem>();
                saveSystem?.UpdateLevelProgress();
            }
        }
        else if (!_isInteractable)
        {
            Debug.Log($"CubeButton '{name}' is locked.");
        }
    }

    public void SetInteractable(bool interactable)
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }

        _isInteractable = interactable;

        _renderer.material = interactable ? _unlockedMaterial : _lockedMaterial;
    }
}