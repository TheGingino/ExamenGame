using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlockPrevention : MonoBehaviour
{
    public  static DeadlockPrevention Instance { get; private set; }
    
    private bool _isProcessing = false;
    private MatchTiles _matchTiles;
    private SpawnTiles _spawnTiles;
    private GridSystem _gridSystem;

    private void Awake() => Instance = this;

    private void Start()
    {
        _matchTiles = GetComponent<MatchTiles>();
        _spawnTiles = GetComponent<SpawnTiles>();
        _gridSystem = GetComponent<GridSystem>();
    }

    public bool IsProcessing => _isProcessing;

    public bool TryBegin()
    {
        if (_isProcessing) return false;
        _isProcessing = true;
        return true;
    }

    public void End() => _isProcessing = false;

    public void TriggerDropAndRefill()
    {
        if(_isProcessing) return;
        StartCoroutine(DropAndRefill());
    }

    private IEnumerator DropAndRefill()
    {
        if(!TryBegin()) yield break;

        bool hadMatch = true;
        while (hadMatch)
        {
            yield return new WaitForSeconds(_spawnTiles.spawnDelay + 0.1f);

            hadMatch = _matchTiles.HasMatches();
            if (hadMatch)
            {
                hadMatch = _matchTiles.HasMatches();
                yield return new WaitForSeconds(_spawnTiles.spawnDelay + 0.1f);
            }
        }
        End();
    }
}
