using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : UsableItem
{
    private TileGravity _tileGravity;
    private GridSystem _gridSystem;
    
    private void Start()    
    {
        _tileGravity = FindObjectOfType<TileGravity>();
        _gridSystem = FindObjectOfType<GridSystem>();
    }
    
    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        OnDrag();
    }

    public override void Use()
    {
        base.Use();
        for (int x = 0; x < _gridSystem.width; x++)
        {
            for (int y = 0; y < _gridSystem.height; y++)
            {
                _tileGravity.DestroyTileAt(x, y);
                Debug.Log($"Hovering over tile at ({x}, {y})");
            }
        }
        Debug.Log("Bomb used! Implement explosion effect."); 
    }
    
    public override void OnDrag()
    {
        base.OnDrag();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPos = _gridSystem.GetGridPosition(mouseWorldPos);
        Debug.Log($"Hovering over tile at ({gridPos.x}, {gridPos.y})");
    }

    private void OnMouseDown()
    {
        Use();
    }
}
