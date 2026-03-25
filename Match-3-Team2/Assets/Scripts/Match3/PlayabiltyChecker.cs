using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayabiltyChecker : MonoBehaviour
{
    private GridSystem _gridSystem;

    private void Awake()
    {
        _gridSystem = GetComponent<GridSystem>();
    }

    public bool BoardStillPlayable(int x, int y, Tile candidate)
    {
        TileSO[,] grid;

        if (candidate == null)
            grid = BuildCurrentGrid();

        else
            grid = BuildGridWith(x, y, candidate._tileData);

            for (int gx = 0; gx < _gridSystem.width; gx++)
        {
            for (int gy = 0; gy < _gridSystem.height; gy++)
            {
                // check horizontal swap
                if (gx + 1 < _gridSystem.width)
                {
                    Swap(grid, gx, gy, gx + 1, gy);
                    if (GridHasMatch(grid)) { Swap(grid, gx, gy, gx + 1, gy); return true; }
                    Swap(grid, gx, gy, gx + 1, gy);
                }
                //check vertical swap
                if (gy + 1 < _gridSystem.height)
                {
                    Swap(grid, gx, gy, gx, gy + 1);
                    if (GridHasMatch(grid)) { Swap(grid, gx, gy, gx, gy + 1); return true; }
                    Swap(grid, gx, gy, gx, gy + 1);
                }
            }
        }
        Debug.Log("Geen geldige zet gevonden → DEADLOCK");
        return false;
    }
    
    private TileSO[,] BuildCurrentGrid()
    {
        TileSO[,] grid = new TileSO[_gridSystem.width, _gridSystem.height];

        for (int x = 0; x < _gridSystem.width; x++)
        {
            for (int y = 0; y < _gridSystem.height; y++)
            {
                grid[x, y] = GetTypeAt(x, y);
            }
        }

        return grid;
    }

    private TileSO[,] BuildGridWith(int cx, int cy, TileSO candidateData)
    {
        TileSO[,] grid = new TileSO[_gridSystem.width, _gridSystem.height];
        for (int gx = 0; gx < _gridSystem.width; gx++)
        for (int gy = 0; gy < _gridSystem.height; gy++)
            grid[gx, gy] = (gx == cx && gy == cy) ? candidateData : GetTypeAt(gx, gy);
        return grid;
    }

    private bool GridHasMatch(TileSO[,] grid)
    {
        for (int gx = 0; gx < _gridSystem.width; gx++)
        for (int gy = 0; gy < _gridSystem.height; gy++)
        {
            TileSO t = grid[gx, gy];
            if (t == null) continue;
            if (gx + 2 < _gridSystem.width  && grid[gx+1, gy] == t && grid[gx+2, gy] == t) return true;
            if (gy + 2 < _gridSystem.height && grid[gx, gy+1] == t && grid[gx, gy+2] == t) return true;
        }
        return false;
    }

    private void Swap(TileSO[,] grid, int x1, int y1, int x2, int y2)
    {
        TileSO tmp = grid[x1, y1];
        grid[x1, y1] = grid[x2, y2];
        grid[x2, y2] = tmp;
    }

    private TileSO GetTypeAt(int x, int y)
    {
        Vector2 worldPos = _gridSystem.GetWorldPosition(x, y);
        Collider[] hits = Physics.OverlapSphere(worldPos, 0.1f, LayerMask.GetMask("Tile"));
        if (hits.Length > 0) return hits[0].GetComponent<Tile>()?._tileData;
        return null;
    }
}
