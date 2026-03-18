using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;
    private Vector3 originPosition = Vector3.zero;    
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
                Vector2 spawnPosition = GetWorldPosition(x, y);
                GameObject backgroundTile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                backgroundTile.transform.parent = transform;
                backgroundTile.transform.position = spawnPosition;
                backgroundTile.transform.localScale = Vector3.one * cellSize;
                backgroundTile.GetComponent<Renderer>().material.color = Color.black;
                DestroyImmediate(backgroundTile.GetComponent<MeshCollider>());

            }
        }
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        originPosition = new Vector3(-width * cellSize / 2 + cellSize / 2, -height * cellSize / 2 + cellSize / 2, 0
        );
        return new Vector2(originPosition.x + x * cellSize, originPosition.y + y * cellSize);
    }

}
