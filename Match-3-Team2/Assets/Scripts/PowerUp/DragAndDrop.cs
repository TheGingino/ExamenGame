using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging;
    private Vector3 offset;
    
    [SerializeField] private GameObject originalPosition;
    
    [SerializeField] private UnityEvent onDropAction;
    private void OnMouseDown()
    {
        isDragging = false;
        gameObject.transform.position = originalPosition.transform.position;
    }
    
    private void OnMouseDrag()
    {
        isDragging = true;
        if (isDragging)
        {
            transform.position = GetMousePosition() + offset;
        }
    }
    
    private void OnMouseUp()
    {
        isDragging = false;
        onDropAction?.Invoke();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Set this to the distance from the camera to the object
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
