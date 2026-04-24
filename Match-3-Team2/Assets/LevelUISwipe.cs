using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUISwipe : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float dragSensitivity = 0.5f;
    [SerializeField] private float minCameraY = 0f;
    [SerializeField] private float maxCameraY = 10f;
    [SerializeField] private float smoothDamping = 0.1f;
    
    private Vector3 dragStartPosition;
    private Vector3 cameraStartPosition;
    private float cameraVelocity;
    private bool isDragging;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
            cameraStartPosition = mainCamera.transform.position;
        }
        
        if (Input.GetMouseButton(0) && isDragging)
        {
            float dragDelta = Input.mousePosition.y - dragStartPosition.y;
            float worldDragDelta = dragDelta * dragSensitivity * 0.01f;
            
            Vector3 newCameraPos = cameraStartPosition - Vector3.up * worldDragDelta;
            newCameraPos.y = Mathf.Clamp(newCameraPos.y, minCameraY, maxCameraY);
            
            mainCamera.transform.position = newCameraPos;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}