using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUISwipe : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _dragSensitivity = 0.5f;
    [SerializeField] private float _minCameraY = 0f;
    [SerializeField] private float _maxCameraY = 10f;
    [SerializeField] private float _smoothDamping = 0.1f;

    private Vector3 _dragStartPosition;
    private Vector3 _cameraStartPosition;
    private float _cameraVelocity;
    private bool _isDragging;

    private void Start()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _dragStartPosition = Input.mousePosition;
            _cameraStartPosition = _mainCamera.transform.position;
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            float dragDelta = Input.mousePosition.y - _dragStartPosition.y;
            float worldDragDelta = dragDelta * _dragSensitivity * 0.01f;

            Vector3 newCameraPos = _cameraStartPosition - Vector3.up * worldDragDelta;
            newCameraPos.y = Mathf.Clamp(newCameraPos.y, _minCameraY, _maxCameraY);

            _mainCamera.transform.position = newCameraPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }
}