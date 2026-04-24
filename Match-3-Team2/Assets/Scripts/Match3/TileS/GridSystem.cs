using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;

    [SerializeField] private GameObject backgroundSprite;
    
    private Vector3 originPosition = Vector3.zero;

    [SerializeField] private float gridOffset;
    [SerializeField] private float gridOffsetX;

    private void Start()
    {
        SetBackground();
    }
    
    private void SetBackground()
    {
        Vector2 gridCenter = new Vector2(
            -width * cellSize / 2 + cellSize / 2 + gridOffsetX, // Apply offset
            -height * cellSize / 2 + cellSize / 2);

        GameObject background = Instantiate(backgroundSprite, new Vector3(gridCenter.x, gridCenter.y, 1f), Quaternion.identity, transform);

        // Scale the sprite to cover the entire grid
        background.transform.localScale = new Vector3(width * cellSize / 10, height * cellSize / 10, 1f);
    }


    public Vector2 GetWorldPosition(int x, int y)
    {
        originPosition = new Vector3(
            -width * cellSize / 2 + cellSize / 2,
            -height * cellSize / 2 + cellSize / 2 + gridOffset,
            0);
        return new Vector2(originPosition.x + x * cellSize, originPosition.y + y * cellSize);
    }

    // \*Safe version, using physics instead of name search\*
    public Tile GetTile(int x, int y)
    {
        Vector2 worldPos = GetWorldPosition(x, y);
        // Use OverlapBox instead of OverlapSphere since tiles have BoxColliders
        Collider[] hits = Physics.OverlapBox(worldPos, Vector3.one * 0.4f, Quaternion.identity, LayerMask.GetMask("Tile"));
        if (hits.Length > 0)
        {
            return hits[0].GetComponent<Tile>();
        }
        return null;
    }

    public Vector2Int GetGridPosition(Vector2 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellSize);
        return new Vector2Int(x, y);
    }
}
