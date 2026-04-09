using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;

    [SerializeField] private Sprite[] backgroundSprites;
    
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
                Vector2 spawnPosition = GetWorldPosition(x, y);
                GameObject backgroundTile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                backgroundTile.transform.parent = transform;
                backgroundTile.transform.position = spawnPosition;
                backgroundTile.transform.localScale = Vector3.one * cellSize;
                backgroundTile.GetComponent<Renderer>().material.color = Color.black;
                DestroyImmediate(backgroundTile.GetComponent<MeshCollider>());
                    
                    
                /*for (int i = 0; i < backgroundSprites.Length; i++)
                {
                    var texture = Random.Range(0, backgroundSprites.Length);
                    GameObject backgroundTile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    backgroundTile.transform.parent = transform;
                    backgroundTile.transform.position = spawnPosition;
                    backgroundTile.transform.localScale = Vector3.one * cellSize;
                    backgroundTile.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
                    backgroundTile.GetComponent<MeshRenderer>().material.mainTexture = backgroundSprites[texture].texture;
                }*/

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
