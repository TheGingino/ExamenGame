using System;
using System.Collections;
using UnityEngine;

public class TileGravity : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float fallCheckInterval = 0.1f;

    private GridSystem _gridSystem;
    private Tile[,] _tileGrid;
    private bool _isApplying;

    private void Start()
    {
        _gridSystem = GetComponent<GridSystem>();
        _tileGrid = new Tile[_gridSystem.width, _gridSystem.height];
        RefreshGravityGrid();
        StartCoroutine(GravityLoop());
    }

    public void RefreshGravityGrid() // Made public
    {
        for (int x = 0; x < _gridSystem.width; x++)
        {
            for (int y = 0; y < _gridSystem.height; y++)
            {
                _tileGrid[x, y] = _gridSystem.GetTile(x, y);
            }
        }
    }

    private IEnumerator GravityLoop()
    {
        while (true)
        {
            if (!_isApplying)
            {
                bool movedAny = ApplyGravityOnce();
                if (movedAny)
                {
                    yield return new WaitForSeconds(fallCheckInterval);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f); // Small delay before checking again
                }
            }
            yield return null;
        }
    }

    private bool ApplyGravityOnce()
    {
        _isApplying = true;
        bool anyMoved = false;

        // Scan from bottom to top to prevent conflicts
        for (int y = 0; y < _gridSystem.height - 1; y++)
        {
            for (int x = 0; x < _gridSystem.width; x++)
            {
                if (_tileGrid[x, y] == null)
                {
                    // Find the closest tile above
                    for (int yAbove = y + 1; yAbove < _gridSystem.height; yAbove++)
                    {
                        if (_tileGrid[x, yAbove] != null)
                        {
                            Tile tileToMove = _tileGrid[x, yAbove];
                            _tileGrid[x, y] = tileToMove;
                            _tileGrid[x, yAbove] = null;
                            TileFall(tileToMove, x, y);
                            anyMoved = true;
                            break; // Only move one tile per column per pass
                        }
                    }
                }
            }
        }

        _isApplying = false;
        return anyMoved;
    }
    
    public IEnumerator ApplyGravityContinuously()
    {
        bool movedAny = true;
        while (movedAny)
        {
            movedAny = ApplyGravityOnce();
            yield return new WaitForSeconds(fallCheckInterval);
        }
    }

    
    private void TileFall(Tile tile, int newX, int newY)
    {
        Vector2 targetPosition = _gridSystem.GetWorldPosition(newX, newY);
        StartCoroutine(AnimateTileFall(tile.gameObject, targetPosition, newX, newY));
    }

    private IEnumerator AnimateTileFall(GameObject tile, Vector2 targetPosition, int finalX, int finalY)
    {
        float fallDuration = 0.3f;
        float elapsedTime = 0f;
        Vector2 startPosition = tile.transform.position;

        while (elapsedTime < fallDuration)
        {
            tile.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tile.transform.position = targetPosition;
        // Only rename AFTER tile reaches destination
        tile.name = $"Tile-({finalX},{finalY})";
    }

    
    public void ClearDestroyedTiles()
    {
        for (int x = 0; x < _gridSystem.width; x++)
        {
            for (int y = 0; y < _gridSystem.height; y++)
            {
                if (_tileGrid[x, y] != null && _tileGrid[x, y].gameObject == null)
                {
                    _tileGrid[x, y] = null;
                }
            }
        }
    }

}