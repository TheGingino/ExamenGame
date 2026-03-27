using System.Collections;
using UnityEngine;

public class SwapTiles : MonoBehaviour
{
    [SerializeField] private float swapDistanceThreshold;

    // Click-to-swap state
    private GameObject firstTile;
    private GameObject secondTile;
    private bool isSwapping;
    private bool _inputDisabled = false;

    // Drag-to-swap state
    private GameObject _draggedTile;
    private bool _isDragging = false;

    private MatchTiles _matchTiles;
    private GridSystem _gridSystem;
    private TileGravity _tileGravity;
    private TurnManager _turnManager;

    private void Start()
    {
        _matchTiles  = GetComponent<MatchTiles>();
        _gridSystem  = GetComponent<GridSystem>();
        _tileGravity = GetComponent<TileGravity>();
        _turnManager = FindObjectOfType<TurnManager>();
    }

    private void Update()
    {
        HandleClickSwap();
        HandleDrag();
    }
    
    private void HandleClickSwap()
    {
        if (isSwapping || _inputDisabled || _isDragging) return;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedTile = GetTileFromScreenPos(Input.mousePosition);
            if (clickedTile == null) return;

            if (firstTile == null)
            {
                firstTile = clickedTile;
            }
            else if (secondTile == null && clickedTile != firstTile)
            {
                secondTile = clickedTile;

                if (AreAdjacent(firstTile, secondTile))
                    StartCoroutine(Swap());
                else
                    ResetSelection();
            }
        }
    }
    
    private void HandleDrag()
    {
        if (isSwapping || _inputDisabled) return;

        // --- Touch (telefoon) ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TryBeginDrag(touch.position);
                    break;

                case TouchPhase.Moved:
                    TryCompleteDrag(touch.position);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    ResetSelection();
                    break;
            }
            return;
        }

        // --- Muis (editor/PC) ---
        if (Input.GetMouseButtonDown(0))
            TryBeginDrag(Input.mousePosition);

        if (_isDragging && Input.GetMouseButton(0))
            TryCompleteDrag(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
            ResetSelection();
    }

    private void TryBeginDrag(Vector2 screenPos)
    {
        GameObject tile = GetTileFromScreenPos(screenPos);
        if (tile == null) return;

        _draggedTile = tile;
        _isDragging  = true;
        firstTile    = tile;
    }

    private void TryCompleteDrag(Vector2 screenPos)
    {
        if (!_isDragging || _draggedTile == null) return;

        GameObject target = GetTileFromScreenPos(screenPos);

        // Zodra de vinger een andere aangrenzende tile raakt → swap
        if (target != null && target != _draggedTile && AreAdjacent(_draggedTile, target))
        {
            secondTile  = target;
            _isDragging = false;
            StartCoroutine(Swap());
        }
    }
    
    private IEnumerator Swap()
    {
        isSwapping = true;
        _matchTiles.SetSwappingState(true);

        // Sla posities en grid-coordinaten op voordat er iets verandert
        Vector3 firstPos  = firstTile.transform.position;
        Vector3 secondPos = secondTile.transform.position;

        Vector2Int gridPosA = _gridSystem.GetGridPosition(firstPos);
        Vector2Int gridPosB = _gridSystem.GetGridPosition(secondPos);

        GameObject tileA = firstTile;
        GameObject tileB = secondTile;

        // Animeer de visuele swap
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

        // Update het grid
        Tile tileComponentA = _tileGravity._TileGrid[gridPosA.x, gridPosA.y];
        Tile tileComponentB = _tileGravity._TileGrid[gridPosB.x, gridPosB.y];
        _tileGravity._TileGrid[gridPosA.x, gridPosA.y] = tileComponentB;
        _tileGravity._TileGrid[gridPosB.x, gridPosB.y] = tileComponentA;

        bool validSwap = false;

        if (!_matchTiles.HasMatches())
        {
            // Geen match → swap terugdraaien
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

            _tileGravity._TileGrid[gridPosA.x, gridPosA.y] = tileComponentA;
            _tileGravity._TileGrid[gridPosB.x, gridPosB.y] = tileComponentB;
        }
        else
        {
            validSwap = true;
        }

        isSwapping = false;
        _matchTiles.SetSwappingState(false);
        ResetSelection();

        _matchTiles.TriggerMatchCheck();

        if (validSwap && _turnManager != null)
            _turnManager.RegisterSwap();
    }
    
    private GameObject GetTileFromScreenPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("Tile"));

        foreach (RaycastHit hit in hits)
            if (hit.collider != null && hit.collider.GetComponent<Tile>() != null)
                return hit.collider.gameObject;

        return null;
    }

    private bool AreAdjacent(GameObject tile1, GameObject tile2)
    {
        return Vector3.Distance(tile1.transform.position, tile2.transform.position) < swapDistanceThreshold;
    }

    private void ResetSelection()
    {
        firstTile    = null;
        secondTile   = null;
        _draggedTile = null;
        _isDragging  = false;
    }

    public void SetInputState(bool state)
    {
        _inputDisabled = !state;
    }
}