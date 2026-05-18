using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileGravity : MonoBehaviour
{
    [SerializeField] private float fallCheckInterval = 0.1f;

    private GridSystem _gridSystem;
    private Tile[,] _tileGrid;
    private bool _isApplying;
    private bool _paused;
    private int _animatingCount;

    // Tiles waiting above the grid to fall in
    private readonly List<PendingTile> _pendingTiles = new();
    private Coroutine _immediateCoroutine; // handle for immediate gravity coroutine
    
    private struct PendingTile
    {
        public Tile tile;
        public int column;
    }

    public Tile[,] _TileGrid => _tileGrid;
    public bool IsAnimating => _animatingCount > 0;
    
    private void Awake()
    {
        _gridSystem = GetComponent<GridSystem>();
        _tileGrid = new Tile[_gridSystem.width, _gridSystem.height];
    }

    private void Start()
    {
        StartCoroutine(GravityLoop());
    }
    
    public void SetPaused(bool paused) => _paused = paused;

    public void RegisterTile(int x, int y, Tile tile)
    {
        _tileGrid[x, y] = tile;
    }
    
    public void EnqueueTile(Tile tile, int column)
    {
        if (tile == null) return;

        _pendingTiles.Add(new PendingTile { tile = tile, column = column });
    
        // Ensure only one ImmediateGravity coroutine is running at a time
        if (_immediateCoroutine != null)
        {
            StopCoroutine(_immediateCoroutine);
            _immediateCoroutine = null;
        }
        _immediateCoroutine = StartCoroutine(ImmediateGravity());
    }

    private IEnumerator ImmediateGravity()
    {
        bool _moved = true;
        while (_moved || _pendingTiles.Count > 0)
        {
            _moved = ApplyGravityOnce();
            yield return new WaitForSeconds(fallCheckInterval);
        }

        // mark immediate coroutine as finished
        _immediateCoroutine = null;
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x < 0 || x >= _gridSystem.width || y < 0 || y >= _gridSystem.height)
            return null;
        return _tileGrid[x, y];
    }

    public void DestroyTileAt(int x, int y)
    {
        Tile t = _tileGrid[x, y];
        if (t != null)
        {
            t.DestroyTile();
            _tileGrid[x, y] = null;
            StartCoroutine(t.PlayAnimationAndDestroy());
        }
    }

    public IEnumerator WaitForAnimations()
    {
        while (_animatingCount > 0 || _pendingTiles.Count > 0)
            yield return null;
    }

    public IEnumerator ApplyGravityContinuously()
    {
        // clean up any destroyed references first
        PurgeDestroyedTiles();

        bool moved = true;
        while (moved || _pendingTiles.Count > 0)
        {
            moved = ApplyGravityOnce();
            // after a pass, purge any destroyed references that may have been left behind
            PurgeDestroyedTiles();
            yield return new WaitForSeconds(fallCheckInterval);
        }
        // Final wait for animations to visually complete
        yield return WaitForAnimations();
    }


    private IEnumerator GravityLoop()
    {
        while (true)
        {
            if (!_paused && !_isApplying)
                ApplyGravityOnce();

            yield return new WaitForSeconds(fallCheckInterval);
        }
    }
    
    private bool ApplyGravityOnce()
    {
        _isApplying = true;
        SetPaused(true);

        bool anyMoved = false;

        // Make sure destroyed objects are removed from the grid references
        PurgeDestroyedTiles();

        for (int y = 0; y < _gridSystem.height - 1; y++)
        {
            for (int x = 0; x < _gridSystem.width; x++)
            {
                if (_tileGrid[x, y] != null) continue;

                for (int above = y + 1; above < _gridSystem.height; above++)
                {
                    if (_tileGrid[x, above] != null)
                    {
                        Tile t = _tileGrid[x, above];
                        _tileGrid[x, y] = t;
                        _tileGrid[x, above] = null;
                        StartCoroutine(AnimateFall(t, x, y));
                        anyMoved = true;
                        break; // keep this — only fill one slot per empty cell
                    }
                }
            }
        }

        // Pull in pending tiles
        List<PendingTile> stillPending = new();
        foreach (PendingTile pending in _pendingTiles)
        {
            if (pending.tile == null || pending.tile.gameObject == null)
                continue; // skip destroyed pending tiles

            int col = pending.column;
            int emptyRow = -1;
            for (int y = _gridSystem.height - 1; y >= 0; y--)
            {
                if (_tileGrid[col, y] == null)
                {
                    emptyRow = y;
                    break;
                }
            }

            if (emptyRow >= 0)
            {
                _tileGrid[col, emptyRow] = pending.tile;
                StartCoroutine(AnimateFall(pending.tile, col, emptyRow));
                anyMoved = true;
            }
            else
            {
                stillPending.Add(pending);
            }
        }
        _pendingTiles.Clear();
        _pendingTiles.AddRange(stillPending);

        _isApplying = false;
        SetPaused(false);
        return anyMoved;
    }
    
    private IEnumerator AnimateFall(Tile tile, int targetX, int targetY)
    {
        _animatingCount++;

        Vector2 start = tile.transform.position;
        Vector2 end   = _gridSystem.GetWorldPosition(targetX, targetY);
        float duration = 0.25f;
        float t = 0f;

        while (t < duration)
        {
            if (tile == null || tile.gameObject == null) { _animatingCount--; yield break; }
            tile.transform.position = Vector2.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        if (tile != null && tile.gameObject != null)
        {
            tile.transform.position = end;
            tile.name = $"Tile-({targetX},{targetY})";
        }

        _animatingCount--;
    }
    
    public void PurgeDestroyedTiles()
    {
        for (int x = 0; x < _gridSystem.width; x++)
            for (int y = 0; y < _gridSystem.height; y++)
                if (_tileGrid[x, y] != null && _tileGrid[x, y].gameObject == null)
                    _tileGrid[x, y] = null;
    }
}