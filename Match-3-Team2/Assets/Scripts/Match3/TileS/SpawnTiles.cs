using System.Collections;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    [SerializeField] private Tile[] _tilePrefabs;
    [SerializeField] public float spawnDelay = 0.1f;

    private GridSystem _gridSystem;
    private TileGravity _tileGravity;
    private Transform _gridTransform;

    private void Awake()
    {
        _gridSystem = GetComponent<GridSystem>();
        _tileGravity = GetComponent<TileGravity>();
        _gridTransform = transform;
    }

    private void Start()
    {
        SpawnInitialTiles();
    }

    // Initial spawn — tiles go directly into their slots, no gravity needed
    private void SpawnInitialTiles()
    {
        for (int x = 0; x < _gridSystem.width; x++)
            for (int y = 0; y < _gridSystem.height; y++)
                SpawnDirect(x, y);
    }

    private void SpawnDirect(int x, int y)
    {
        Tile prefab = GetNonMatchingTile(x, y);
        Vector2 pos = _gridSystem.GetWorldPosition(x, y);
        GameObject obj = Instantiate(prefab.gameObject, pos, Quaternion.identity, _gridTransform);
        obj.name = $"Tile-({x},{y})";

        Tile tile = obj.GetComponent<Tile>();
        _tileGravity.RegisterTile(x, y, tile);

        Debug.Assert(_tileGravity.GetTileAt(x, y) != null, $"RegisterTile failed at ({x},{y})");
    }

    // Post-match spawn — enqueue above the grid, gravity pulls them down
    public IEnumerator FillColumn(int col, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = _gridSystem.GetWorldPosition(col, _gridSystem.height - 1);
            spawnPos.y += _gridSystem.cellSize * (i + 1);

            Tile prefab = GetNonMatchingTile(col, _gridSystem.height - 1);
            GameObject obj = Instantiate(prefab.gameObject, spawnPos, Quaternion.identity, _gridTransform);
            obj.name = $"Tile-pending-({col})";

            _tileGravity.EnqueueTile(obj.GetComponent<Tile>(), col);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Tile GetNonMatchingTile(int x, int y)
    {
        Tile selected;
        int attempts = 0;
        do
        {
            selected = _tilePrefabs[Random.Range(0, _tilePrefabs.Length)];
            attempts++;
        } while (attempts < 100 && WouldMatch(x, y, selected));
        return selected;
    }

    private bool WouldMatch(int x, int y, Tile tile)
    {
        var d = tile.TileData;
        if (x >= 2 && _tileGravity.GetTileAt(x - 1, y)?.TileData == d && _tileGravity.GetTileAt(x - 2, y)?.TileData == d) return true;
        if (x <= _gridSystem.width - 3 && _tileGravity.GetTileAt(x + 1, y)?.TileData == d && _tileGravity.GetTileAt(x + 2, y)?.TileData == d) return true;
        if (y >= 2 && _tileGravity.GetTileAt(x, y - 1)?.TileData == d && _tileGravity.GetTileAt(x, y - 2)?.TileData == d) return true;
        if (y <= _gridSystem.height - 3 && _tileGravity.GetTileAt(x, y + 1)?.TileData == d && _tileGravity.GetTileAt(x, y + 2)?.TileData == d) return true;
        return false;
    }
}