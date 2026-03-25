using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTiles : MonoBehaviour
{

    [SerializeField] private float swapDistanceThreshold;
    
    private GameObject firstTile;
    private GameObject secondTile;
    private bool isSwapping;
    private bool _inputDisabled = false;
    
    private MatchTiles _matchTiles;

    private void Start()
    {
        _matchTiles = GetComponent<MatchTiles>();
    }

    private void Update()
    {
        MoveTiles();
    }

    private void MoveTiles()
    {
        if (isSwapping) return;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedTile = GetClickedTile();
            if (clickedTile != null)
            {
                if (firstTile == null)
                {
                    firstTile = clickedTile;
                }
                else if (secondTile == null && clickedTile != firstTile)
                {
                    secondTile = clickedTile;

                    if (AreAdjacent(firstTile, secondTile))
                    {
                        StartCoroutine(Swap());
                        //DragTiles();
                    }
                    else
                    {
                        ResetSelection();
                    }
                }
            }
        }
    }
    
    public GameObject GetClickedTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("Tile"));
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<Tile>() != null)
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }
    
    private IEnumerator Swap()
    {
        isSwapping = true;
        
        //Added to remove MissingReference
        _matchTiles.SetSwappingState(true);
        
        //already existing logic
        Vector3 firstPos = firstTile.transform.position;
        Vector3 secondPos = secondTile.transform.position;

        float swapDuration = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < swapDuration)
        {
            firstTile.transform.position = Vector3.Lerp(firstPos, secondPos, elapsedTime / swapDuration);
            secondTile.transform.position = Vector3.Lerp(secondPos, firstPos, elapsedTime / swapDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        firstTile.transform.position = secondPos;
        secondTile.transform.position = firstPos;
        
        if (!_matchTiles.HasMatches())
        {
            // Swap back if no match
            elapsedTime = 0f;
            while (elapsedTime < swapDuration)
            {
                firstTile.transform.position = Vector3.Lerp(secondPos, firstPos, elapsedTime / swapDuration);
                secondTile.transform.position = Vector3.Lerp(firstPos, secondPos, elapsedTime / swapDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            firstTile.transform.position = firstPos;
            secondTile.transform.position = secondPos; 
            
            isSwapping = false;
            ResetSelection();
            
        }
        isSwapping = false;
        ResetSelection();
        MatchCheck();  
    }
    
    private void DragTiles()
    {
        // Implement drag logic here
        
        //can only move to the adjacent tile
        
        ResetSelection();
    }
    
    private bool AreAdjacent(GameObject tile1, GameObject tile2)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        float distance = Vector3.Distance(pos1, pos2);
        return distance < swapDistanceThreshold;
    }
    
    private void MatchCheck()
    {
        _matchTiles.SetSwappingState(false);
        _matchTiles.TriggerMatchCheck();
    }

    private void ResetSelection()
    {
        firstTile = null;
        secondTile = null;
    }
}