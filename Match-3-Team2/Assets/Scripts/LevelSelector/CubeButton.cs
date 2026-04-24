using System;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] private string _buttonID;
    [SerializeField] private LevelSelector _levelSelector;
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Material unlockedMaterial;
    
    private bool _isInteractable = true;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        
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
        _isInteractable = interactable;
        _renderer.material = interactable ? unlockedMaterial : lockedMaterial;
    }
}