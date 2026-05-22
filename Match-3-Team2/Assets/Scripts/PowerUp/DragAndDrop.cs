using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragAndDrop : MonoBehaviour
{
    private bool _isDragging;
    private Vector3 _offset;

    [SerializeField] private GameObject _originalPosition;
    [SerializeField] private UnityEvent _onDropAction;
    private void OnMouseDown()
    {
        _isDragging = false;
        gameObject.transform.position = _originalPosition.transform.position;
    }
    
    private void OnMouseDrag()
    {
        _isDragging = true;
        if (_isDragging)
        {
            transform.position = GetMousePosition() + _offset;
        }
    }
    
    private void OnMouseUp()
    {
        _isDragging = false;
        _onDropAction?.Invoke();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Set this to the distance from the camera to the object
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
