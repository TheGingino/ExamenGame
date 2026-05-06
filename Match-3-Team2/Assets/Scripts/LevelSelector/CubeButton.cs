using System;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] private string _buttonID;
    [SerializeField] private LevelSelector _levelSelector;
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Material unlockedMaterial;
    
    private bool _isInteractable;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _isInteractable = true; // Default to interactable, will be set by SaveSystem
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
        
            // Testing: increment level progress on first button
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
        _renderer.material = interactable ? unlockedMaterial : lockedMaterial;
    }
}