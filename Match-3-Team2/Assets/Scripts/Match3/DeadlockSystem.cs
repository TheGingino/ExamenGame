using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeadlockSystem : MonoBehaviour
{
    private GridSystem _gridSystem;
    private MatchTiles _matchTiles;

    

    [SerializeField] private GameObject shuffleTextUI;

    private void Start()
    {
        _gridSystem = GetComponent<GridSystem>();
        _matchTiles = GetComponent<MatchTiles>();
        GameEvents.OnBoardStable+= CheckForDeadLock;
    }

    private void OnDestroy()
    {
        GameEvents.OnBoardStable -= CheckForDeadLock;
    }
    private bool _isChecking = false;
    private void CheckForDeadLock()
    
    {
        if (_isChecking) return;
            StartCoroutine(ShuffleRoutine());
    }

    private IEnumerator CheckRoutine()
    {
        _isChecking = true;
        yield return new WaitForSeconds(0.1f);

        if (!HasValidMove())
            yield return StartCoroutine(ShuffleRoutine());

        _isChecking = false;
    }

    private bool HasValidMove()
    {
        for (int x = 0; x < _gridSystem.width; x++)
        {
            for (int y = 0; y < _gridSystem.height; y++)
            {
                if (x + 1 < _gridSystem.width)
                {
                    SwapData (x,y, x +1, y);
                    bool match = HasMatchAt(x, y) || HasMatchAt(x + 1, y);
                    SwapData(x, y, x + 1, y);
                    if (match) return true;
                }
                if (y + 1 < _gridSystem.width)
                {
                    SwapData (x,y,x,y +1);
                    bool match = HasMatchAt(x, y) || HasMatchAt(x, y + 1);
                    SwapData(x, y, x, y + 1);
                    if (match) return true;
                }
            }
        }

        return false;
    }

    private bool HasMatchAt(int x, int y)
    {
        Tile tile = GetTileAt(x, y);
        if (tile == null) return false;
        TileSO type = tile._tileData;

        int h = 1;
        if (x - 1 >= 0 && GetTileAt(x -1, y)?._tileData == type) h++;
        if (x + 1 < _gridSystem.width && GetTileAt(x + 1, y)?._tileData == type) h++;
        if (h >= 3) return true;

        int v = 1;
        if (y - 1 >= 0 && GetTileAt(x, y -1)?._tileData == type) h++;
        if (y + 1 < _gridSystem.width && GetTileAt(x, y + 1)?._tileData == type) h++;
        return v >= 3;
    }

    private void SwapData(int x1,int y1,int x2,int y2)
    {
        Tile a = GetTileAt(x1 ,y1);
        Tile b = GetTileAt(x2, y1);
        if (a == null || b == null) return;

        TileSO temp = a._tileData;
        a.SetType(b._tileData);
        b.SetType(temp);
    }

    private IEnumerator ShuffleRoutine()
    {
        _matchTiles.SetSwappingState(true);
        GameEvents.InputDisabled();
        
        if (shuffleTextUI != null) shuffleTextUI.SetActive(true);
        yield return new WaitForSeconds(1f);

        int tries = 0;
        do
        {
            yield return StartCoroutine(ShuffleGrid());
            yield return new WaitForSeconds(0.2f);
            tries++;
            
        } while (!HasValidMove() && tries < 10);
        
        if (shuffleTextUI != null) shuffleTextUI.SetActive(false);
        _matchTiles.SetSwappingState(false);
        GameEvents.InputDisabled();
    }

    private IEnumerator ShuffleGrid()
    {
        List<Tile> tiles = new List<Tile>();
        List<Vector2> positions = new List<Vector2>();
        
        for(int x = 0; x< _gridSystem.width; x++)
        for (int y = 0; y < _gridSystem.height; y++)
        {
            Tile t = GetTileAt(x, y);
            if (t != null)
            {
                tiles.Add(t);
                positions.Add(_gridSystem.GetWorldPosition(x, y));
            }
        }

        for (int i = positions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (positions[i], positions[j]) = (positions[j], positions[i]);
        }

        float duration = 0.4f;
        float elapsed = 0f;
        List<Vector3> startPositions = new List<Vector3>();
        foreach (Tile t in tiles)
            startPositions.Add(t.transform.position);

        while (elapsed < duration)
        {
            for(int i = 0; i< tiles.Count; i++)
                if(tiles[i ]!= null)
                    tiles[i].transform.position = Vector3.Lerp(startPositions[i], positions[i],elapsed/ duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        for(int i= 0;i < tiles.Count; i++)
            if (tiles[i] != null)
                tiles[i].transform.position = positions[i];
    }

    private Tile GetTileAt(int x, int y)
    {
        Vector2 worldPos = _gridSystem.GetWorldPosition(x, y);
        Collider[] hits = Physics.OverlapSphere(worldPos, 0.1f, LayerMask.GetMask("Tile"));
        if (hits.Length > 0) return hits[0].GetComponent<Tile>();
        return null;
    }
}
