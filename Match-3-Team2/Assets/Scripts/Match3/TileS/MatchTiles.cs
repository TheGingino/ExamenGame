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
    }
    
    private IEnumerator ResolveMatches(List<Vector2Int>matches)
    {
        _tileGravity.SetPaused(true);
        
        int totalHeal = 0;
        int totalDamage = 0; // <- attack but the value is the damage
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
        
        // Feed totals into the meters
        if (totalHeal    > 0) CombatMeter.Instance.Add(TileType.Heal,    totalHeal);
        if (totalDamage  > 0) CombatMeter.Instance.Add(TileType.Damage,  totalDamage);
        if (totalShield   > 0) CombatMeter.Instance.Add(TileType.Shield,  totalShield);
        if (totalSpecial > 0) CombatMeter.Instance.Add(TileType.Special, totalSpecial);

        // Track cleared tiles per column for respawning
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

        yield return _tileGravity.WaitForAnimations();
        yield return new WaitForSeconds(0.1f);

        CheckForMatches();
    }
}