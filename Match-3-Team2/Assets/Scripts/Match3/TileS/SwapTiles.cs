using System.Collections;
using UnityEngine;

public class SwapTiles : MonoBehaviour
{
    [SerializeField] private float swapDistanceThreshold;
    
    private GameObject firstTile;
    private GameObject secondTile;
    private bool isSwapping;
    private bool _inputDisabled = false;
    
    private MatchTiles _matchTiles;
    private GridSystem _gridSystem;
    private TileGravity _tileGravity;
    
    private void Start()
    {
        _matchTiles = GetComponent<MatchTiles>();
        _gridSystem = GetComponent<GridSystem>();
        _tileGravity = GetComponent<TileGravity>();
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
        _matchTiles.SetSwappingState(true);

        // ── Cache everything at the START before anything can be destroyed ──
        Vector3 firstPos  = firstTile.transform.position;
        Vector3 secondPos = secondTile.transform.position;
        
        Vector2Int gridPosA = _gridSystem.GetGridPosition(firstPos);
        Vector2Int gridPosB = _gridSystem.GetGridPosition(secondPos);
        
        // Hold local refs so we don't rely on the fields staying valid
        GameObject tileA = firstTile;
        GameObject tileB = secondTile;

        // ── Animate visual swap ──
        float swapDuration = 0.2f;
        float elapsed = 0f;
        while (elapsed < swapDuration)
        {
            if (tileA == null || tileB == null) { isSwapping = false; ResetSelection(); yield break; }
            tileA.transform.position = Vector3.Lerp(firstPos,  secondPos, elapsed / swapDuration);
            tileB.transform.position = Vector3.Lerp(secondPos, firstPos,  elapsed / swapDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (tileA == null || tileB == null) { isSwapping = false; ResetSelection(); yield break; }
        tileA.transform.position = secondPos;
        tileB.transform.position = firstPos;

        // ── Update _tileGrid to reflect the swap ──
        Tile tileComponentA = _tileGravity._TileGrid[gridPosA.x, gridPosA.y];
        Tile tileComponentB = _tileGravity._TileGrid[gridPosB.x, gridPosB.y];
        _tileGravity._TileGrid[gridPosA.x, gridPosA.y] = tileComponentB;
        _tileGravity._TileGrid[gridPosB.x, gridPosB.y] = tileComponentA;

        // ── Check for matches against updated grid ──
        if (!_matchTiles.HasMatches())
        {
            // No match — swap back visually
            elapsed = 0f;
            while (elapsed < swapDuration)
            {
                if (tileA == null || tileB == null) break;
                tileA.transform.position = Vector3.Lerp(secondPos, firstPos,  elapsed / swapDuration);
                tileB.transform.position = Vector3.Lerp(firstPos,  secondPos, elapsed / swapDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            if (tileA != null) tileA.transform.position = firstPos;
            if (tileB != null) tileB.transform.position = secondPos;

            // Revert grid too
            _tileGravity._TileGrid[gridPosA.x, gridPosA.y] = tileComponentA;
            _tileGravity._TileGrid[gridPosB.x, gridPosB.y] = tileComponentB;
        }

        isSwapping = false;
        _matchTiles.SetSwappingState(false);
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
        // Return tile if there is no match, otherwise destroy the tile and spawn new one
        _matchTiles.TriggerMatchCheck();
        _matchTiles.SetSwappingState(false);
    }

    private void ResetSelection()
    {
        firstTile = null;
        secondTile = null;
    }
}