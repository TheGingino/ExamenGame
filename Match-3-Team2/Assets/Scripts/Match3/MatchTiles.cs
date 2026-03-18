using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchTiles : MonoBehaviour
{
    private GridSystem _gridSystem;
    private SwapTiles _swapTiles;
    
    private bool _isSwapping = false;
    
    private void Start()
    {
        _gridSystem = GetComponent<GridSystem>();
    }


    private void Update()
    {
        if (!_isSwapping)
        {
            CheckForMatches();
        }
    }
    private void CheckForMatches()
    {
        List<Vector2> matchingTiles = GetMatchingTiles();
        if (matchingTiles.Count >= 3)
        {
            ClearMatches(matchingTiles);
        }
    }

    List<Vector2> GetMatchingTiles()
    {
        HashSet<Vector2> matchingTiles = new();

        // Check Horizontal Matches
        for (int y = 0; y < _gridSystem.height; y++)
        {
            for (int x = 0; x < _gridSystem.width - 2; x++)
            {
                var tileA = GetTileAtPosition(x, y);
                var tileB = GetTileAtPosition(x + 1, y);
                var tileC = GetTileAtPosition(x + 2, y);

                if (tileA != null && tileB != null && tileC != null)
                {
                    if (tileA._tileData == tileB._tileData && tileB._tileData == tileC._tileData)
                    {
                        matchingTiles.Add(new Vector2(x, y));
                        matchingTiles.Add(new Vector2(x + 1, y));
                        matchingTiles.Add(new Vector2(x + 2, y));
                    }
                }
            }
        }

        // Check Vertical Matches
        for (int x = 0; x < _gridSystem.width; x++)
        {
            for (int y = 0; y < _gridSystem.height - 2; y++)
            {
                var tileA = GetTileAtPosition(x, y);
                var tileB = GetTileAtPosition(x, y + 1);
                var tileC = GetTileAtPosition(x, y + 2);

                if (tileA != null && tileB != null && tileC != null)
                {
                    if (tileA._tileData == tileB._tileData && tileB._tileData == tileC._tileData)
                    {
                        matchingTiles.Add(new Vector2(x, y));
                        matchingTiles.Add(new Vector2(x, y + 1));
                        matchingTiles.Add(new Vector2(x, y + 2));
                    }
                }
            }
        }
        return new List<Vector2>(matchingTiles);
    }

    private Tile GetTileAtPosition(int i, int tilePosY)
    {
        Vector2 worldPos = _gridSystem.GetWorldPosition(i, tilePosY);
        Collider[] hits = Physics.OverlapSphere(worldPos, 0.1f, LayerMask.GetMask("Tile"));
    
        if (hits.Length > 0)
        {
            return hits[0].GetComponent<Tile>();
        }
        return null;
    }
    
    private void ClearMatches(List<Vector2> matchingTiles)
    {
        foreach (Vector2 pos in matchingTiles)
        {
            Tile tileToDestroy = GetTileAtPosition((int)pos.x, (int)pos.y);
            if (tileToDestroy != null)
            {
                tileToDestroy.DestroyTile();
            }
        }
    }
    
    // This function is to remove the missing Reference
    public void SetSwappingState(bool swapping)
    {
        _isSwapping = swapping;
    }
}
