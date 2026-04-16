using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTiles : MonoBehaviour
{
    private GridSystem _gridSystem;
    private TileGravity _tileGravity;
    private SpawnTiles _spawnTiles;
    private bool _isSwapping;
    
    private void Start()
    {
        _gridSystem = GetComponent<GridSystem>();
        _tileGravity = GetComponent<TileGravity>();
        _spawnTiles = GetComponent<SpawnTiles>();
    }
    
    public void TriggerMatchCheck() => CheckForMatches();
    public void SetSwappingState(bool swapping) => _isSwapping = swapping;
    public bool HasMatches() => GetMatchingTiles().Count >= 3;
    
    private List<Vector2Int> GetMatchingTiles()
    {
        HashSet<Vector2Int> matches = new();

        // Horizontal
        for (int y = 0; y < _gridSystem.height; y++)
        {
            for (int x = 0; x < _gridSystem.width - 2; x++)
            {
                Tile a = _tileGravity.GetTileAt(x,     y);
                Tile b = _tileGravity.GetTileAt(x + 1, y);
                Tile c = _tileGravity.GetTileAt(x + 2, y);

                if (a != null && b != null && c != null &&
                    a._tileData == b._tileData && b._tileData == c._tileData)
                {
                    matches.Add(new Vector2Int(x,     y));
                    matches.Add(new Vector2Int(x + 1, y));
                    matches.Add(new Vector2Int(x + 2, y));
                }
            }
        }

        // Vertical
        for (int x = 0; x < _gridSystem.width; x++)
        {
            for (int y = 0; y < _gridSystem.height - 2; y++)
            {
                Tile a = _tileGravity.GetTileAt(x, y);
                Tile b = _tileGravity.GetTileAt(x, y + 1);
                Tile c = _tileGravity.GetTileAt(x, y + 2);

                if (a != null && b != null && c != null &&
                    a._tileData == b._tileData && b._tileData == c._tileData)
                {
                    matches.Add(new Vector2Int(x, y));
                    matches.Add(new Vector2Int(x, y + 1));
                    matches.Add(new Vector2Int(x, y + 2));
                }
            }
        }

        return new List<Vector2Int>(matches);
    }
    
    public void CheckForMatches()
    {
        List<Vector2Int> matches = GetMatchingTiles();
        Debug.Log($"CheckForMatches found {matches.Count} matching tiles");
    
        if (matches.Count >= 3)
            StartCoroutine(ResolveMatches(matches));
        else if (!HasValidMoves())
        {
            Debug.Log("Deadlock detected! Clearing and respawning grid.");
            StartCoroutine(ResolveGrid());
        }
    }
    
    private IEnumerator ResolveMatches(List<Vector2Int> matches)
    {
        _tileGravity.SetPaused(true);
        GetComponent<SwapTiles>().SetInputState(false); // Block input

        int totalHeal = 0;
        int totalDamage = 0;
        int totalShield = 0;
        int totalSpecial = 0;

        foreach (Vector2Int pos in matches)
        {
            Tile tile = _tileGravity.GetTileAt(pos.x, pos.y);
            if(tile == null) continue;

            switch (tile._tileData.tileType)
            {
                case TileType.Heal: totalHeal += tile._tileData.HealAmount; break;
                case TileType.Damage: totalDamage += tile._tileData.DamageAmount; break;
                case TileType.Shield: totalShield += tile._tileData.ShieldAmount; break;
                case TileType.Special: totalSpecial += tile._tileData.specialAttackAmount; break;
            }
        }
        Debug.Log($"[MatchTiles] Totalen — Heal: {totalHeal}, Damage: {totalDamage}, Shield: {totalShield}, Special: {totalSpecial}");

        if (totalHeal > 0) CombatMeter.Instance.Add(TileType.Heal, totalHeal);
        if (totalDamage > 0) CombatMeter.Instance.Add(TileType.Damage, totalDamage);
        if (totalShield > 0) CombatMeter.Instance.Add(TileType.Shield, totalShield);
        if (totalSpecial > 0) CombatMeter.Instance.Add(TileType.Special, totalSpecial);

        Dictionary<int, int> clearedPerColumn = new();
        foreach (Vector2Int pos in matches)
        {
            if (!clearedPerColumn.ContainsKey(pos.x))
                clearedPerColumn[pos.x] = 0;
            clearedPerColumn[pos.x]++;
        }

        foreach (Vector2Int pos in matches)
            _tileGravity.DestroyTileAt(pos.x, pos.y);

        yield return _tileGravity.ApplyGravityContinuously();
        yield return _tileGravity.WaitForAnimations();

        foreach (var kvp in clearedPerColumn)
            yield return _spawnTiles.FillColumn(kvp.Key, kvp.Value);

        _tileGravity.SetPaused(false);
        GetComponent<SwapTiles>().SetInputState(true); // Re-enable input

        yield return _tileGravity.WaitForAnimations();
        yield return new WaitForSeconds(0.1f);

        CheckForMatches();
    }
    
    //Deadlock resolution: If there are no valid moves, clear the board and respawn
    private bool HasValidMoves()
    {
        // Check if swapping any two adjacent tiles creates a match
        for (int y = 0; y < _gridSystem.height; y++)
        {
            for (int x = 0; x < _gridSystem.width; x++)
            {
                // Swap Horizontal
                if (x < _gridSystem.width - 1 && WouldCreateMatch(x, y, x + 1, y))
                    return true;
                // Swap Vertical
                if (y < _gridSystem.height - 1 && WouldCreateMatch(x, y, x, y + 1))
                    return true;
            }
        }
        return false;
    }
    
    private bool WouldCreateMatch(int x1, int y1, int x2, int y2)
    {
        Tile tile1 = _tileGravity.GetTileAt(x1, y1);
        Tile tile2 = _tileGravity.GetTileAt(x2, y2);

        if (tile1 == null || tile2 == null)
            return false;

        // Temporarily swap the tiles to check for matches
        (_tileGravity._TileGrid[y1, x1], _tileGravity._TileGrid[y2, x2]) =
            (_tileGravity._TileGrid[y2, x2], _tileGravity._TileGrid[y1, x1]);

        List<Vector2Int> matches = GetMatchingTiles();
        bool hasMatch = matches.Count >= 3;

        // Swap back to restore original state
        (_tileGravity._TileGrid[y1, x1], _tileGravity._TileGrid[y2, x2]) =
            (_tileGravity._TileGrid[y2, x2], _tileGravity._TileGrid[y1, x1]);
        
        return hasMatch;
    }

    private IEnumerator ResolveGrid()
    {
        _tileGravity.SetPaused(true);

        // Clear the tiles
        for (int y = 0; y < _gridSystem.height; y++)
        {
            for (int x = 0; x < _gridSystem.width; x++)
            {
                _tileGravity.DestroyTileAt(x, y);
            }
        }

        yield return _tileGravity.WaitForAnimations();
        
        // Respawn new tiles
        for (int x = 0; x < _gridSystem.width; x++)
        {
            yield return _spawnTiles.FillColumn(x, _gridSystem.height);
        }
        
        _tileGravity.SetPaused(false);
        yield return _tileGravity.WaitForAnimations();
        yield return new WaitForSeconds(0.2f);
        
        CheckForMatches();
    }
}