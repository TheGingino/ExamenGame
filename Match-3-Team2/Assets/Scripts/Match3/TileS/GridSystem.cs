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

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        CreateGrid();
    }
    
    private void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 worldPos = GetWorldPosition(x, y);
                Instantiate(backgroundSprite, worldPos, Quaternion.identity, transform);
            }
        }
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
