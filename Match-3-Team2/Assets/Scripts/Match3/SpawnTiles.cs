using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnTiles : MonoBehaviour
{
    [SerializeField] private Tile[] tilePrefabs;
    [SerializeField] public float spawnDelay = 0.5f;

    private GridSystem _gridSystem;
    private PlayabiltyChecker _playabiltyChecker;
    private Transform _gridTransform;
    
    private void Awake()
    {
        _gridSystem = GetComponent<GridSystem>();
        _playabiltyChecker = GetComponent<PlayabiltyChecker>();
        _gridTransform = transform;
    }
    
    private void Start()
    {
        SpawnInitialTiles();
    }

    private void SpawnInitialTiles()
    {
        for (int x = 0; x < _gridSystem.width; x++)
        {   
            for (int y = 0; y < _gridSystem.height; y++)
            {
                SpawnTile(x, y);
            }
        }
    }

    private void SpawnTile(int x, int y)
    {
        SpawnTileWithNoMatch(x,y);
    }
    
    public IEnumerator SpawnTileWithDelay(int x, int y)
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnTile(x, y);
    }
    
    private void SpawnTileWithNoMatch(int x, int y)
    {
        Tile tilePrefab = GetRandomTileWithoutMatch(x, y);
        Vector2 spawnPosition = _gridSystem.GetWorldPosition(x, y);

        GameObject newTile = Instantiate(tilePrefab.gameObject, spawnPosition, Quaternion.identity, _gridTransform);
        newTile.name = $"Tile_{x}_{y}";
    }

    private Tile GetRandomTileWithoutMatch(int x, int y)
    {
        Tile selectedTile;
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        selectedTile = tilePrefabs[randomIndex];
        int safety = 0;
        while (HasMatch(x, y, selectedTile) || !_playabiltyChecker.BoardStillPlayable(x, y, selectedTile))
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
            selectedTile = tilePrefabs[randomIndex];
        }
        return selectedTile;
    }

    private bool HasMatch(int x, int y, Tile tile)
    {
        // Checks horizontal match
        if (x >= 2 && GetTileTypeAtPosition(x - 1, y) == tile._tileData &&
            GetTileTypeAtPosition(x - 2, y) == tile._tileData)
            return true;

        if (x <= _gridSystem.width - 3 && GetTileTypeAtPosition(x + 1, y) == tile._tileData &&
            GetTileTypeAtPosition(x + 2, y) == tile._tileData)
            return true;

        // Check vertical match
        if (y >= 2 && GetTileTypeAtPosition(x, y - 1) == tile._tileData &&
            GetTileTypeAtPosition(x, y - 2) == tile._tileData)
            return true;

        if (y <= _gridSystem.height - 3 && GetTileTypeAtPosition(x, y + 1) == tile._tileData &&
            GetTileTypeAtPosition(x, y + 2) == tile._tileData)
            return true;

        return false;
    }
    
    private ScriptableObject GetTileTypeAtPosition(int x, int y)
    {
        Vector2 worldPos = _gridSystem.GetWorldPosition(x, y);
        Collider[] hits = Physics.OverlapSphere(worldPos, 0.1f, LayerMask.GetMask("Tile"));

        if (hits.Length > 0)
        {
            return hits[0].GetComponent<Tile>()?._tileData;
        }
        return null;
    }
}