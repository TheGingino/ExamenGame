using System.Collections;
using UnityEngine;

public class SwapTiles : MonoBehaviour
{
    [SerializeField] private float swapDistanceThreshold = 1.1f;
    [SerializeField] private float dragThreshold = 12f;

    private bool _inputDisabled = false;
    private bool _isSwapping = false;

    // Gedeelde pointer-state
    private GameObject _pointerTile;
    private Vector2 _pointerDownPos;
    private bool _dragStarted;
    private bool _swapQueued;

    // Click-to-swap (two Tabs to swap)
    private GameObject _firstClickTile;

    private MatchTiles _matchTiles;
    private GridSystem _gridSystem;
    private TileGravity _tileGravity;
    private TurnManager _turnManager;
    private GameEndManager _gameEndManager;

    private void Start()
    {
        _matchTiles = GetComponent<MatchTiles>();
        _gridSystem = GetComponent<GridSystem>();
        _tileGravity = GetComponent<TileGravity>();
        _turnManager = FindObjectOfType<TurnManager>();
        _gameEndManager = FindObjectOfType<GameEndManager>();
    }

    private void Update()
    {
        if (_gameEndManager != null && !_gameEndManager.IsGameActive())
            return;

        if (_isSwapping || _inputDisabled) return;

        // Touch has priority
        if (Input.touchCount > 0)
        {
            HandleTouch(Input.GetTouch(0));
            return;
        }

        HandleMouse();
    }

    private void HandleTouch(Touch t)
    {
        switch (t.phase)
        {
            case TouchPhase.Began:
                OnPointerDown(t.position);
                break;
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                OnPointerMove(t.position);
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                OnPointerUp(t.position);
                break;
        }
    }
    
    //For pc testing (eddtior)
    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
            OnPointerDown(Input.mousePosition);
        else if (Input.GetMouseButton(0))
            OnPointerMove(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0))
            OnPointerUp(Input.mousePosition);
    }
    
    private void OnPointerDown(Vector2 screenPos)
    {
        _pointerTile    = GetTileAtScreenPos(screenPos);
        _pointerDownPos = screenPos;
        _dragStarted    = false;
        _swapQueued     = false;
    }

    private void OnPointerMove(Vector2 screenPos)
    {
        if (_pointerTile == null || _swapQueued) return;
        
        //If you dragg far away with your finger, dragg modus is activated
        if (!_dragStarted &&
            Vector2.Distance(screenPos, _pointerDownPos) >= dragThreshold)
        {
            _dragStarted = true;
        }

        if (!_dragStarted) return;
        
        //Checks if the pointer touches adjacent tile
        GameObject target = GetTileAtScreenPos(screenPos);
        if (target != null && target != _pointerTile && AreAdjacent(_pointerTile, target))
        {
            _swapQueued = true;
            StartCoroutine(Swap(_pointerTile, target));
        }
    }

    private void OnPointerUp(Vector2 screenPos)
    {
       //If pointer was released without dragging, treat it like a tab
        if (_pointerTile != null && !_dragStarted && !_swapQueued)
        {
            HandleTapSwap(_pointerTile);
        }
        //Rest pointerstate aftehr release, (_firstClickTile is NOT reset here because tap-swap needs it)
        _pointerTile = null;
        _dragStarted = false;
        _swapQueued  = false;
    }

    //Tap swap
    private void HandleTapSwap(GameObject tapped)
    {
        if (_firstClickTile == null)
        {
            _firstClickTile = tapped;
            return;
        }

        if (tapped == _firstClickTile)
        {
            // same tille tapped twice = deselect
            _firstClickTile = null;
            return;
        }

        if (AreAdjacent(_firstClickTile, tapped))
        {
            StartCoroutine(Swap(_firstClickTile, tapped));
        }

        _firstClickTile = null;
    }

    private IEnumerator Swap(GameObject tileA, GameObject tileB)
    {
        if (_gameEndManager != null && !_gameEndManager.IsGameActive())
            yield break;

        _isSwapping = true;
        _matchTiles.SetSwappingState(true);

        Vector3 posA = tileA.transform.position;
        Vector3 posB = tileB.transform.position;
        Vector2Int gridPosA = _gridSystem.GetGridPosition(posA);
        Vector2Int gridPosB = _gridSystem.GetGridPosition(posB);

        yield return AnimateSwap(tileA, tileB, posA, posB, 0.2f);

        if (tileA == null || tileB == null)
        {
            FinishSwap();
            yield break;
        }

        tileA.transform.position = posB;
        tileB.transform.position = posA;

        // Update grid
        Tile compA = _tileGravity._TileGrid[gridPosA.x, gridPosA.y];
        Tile compB = _tileGravity._TileGrid[gridPosB.x, gridPosB.y];
        _tileGravity._TileGrid[gridPosA.x, gridPosA.y] = compB;
        _tileGravity._TileGrid[gridPosB.x, gridPosB.y] = compA;

        bool validSwap = _matchTiles.HasMatches();

        if (!validSwap)
        {
            // No match = Rollback
            yield return AnimateSwap(tileA, tileB, posB, posA, 0.2f);
            if (tileA != null) tileA.transform.position = posA;
            if (tileB != null) tileB.transform.position = posB;
            _tileGravity._TileGrid[gridPosA.x, gridPosA.y] = compA;
            _tileGravity._TileGrid[gridPosB.x, gridPosB.y] = compB;
        }

        FinishSwap();
        _matchTiles.TriggerMatchCheck();

        if (validSwap && _turnManager != null &&
            _gameEndManager != null && _gameEndManager.IsGameActive())
        {
            _turnManager.RegisterSwap();
        }
    }

    private IEnumerator AnimateSwap(GameObject a, GameObject b,
                                    Vector3 fromA, Vector3 fromB, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (a == null || b == null) yield break;
            float t = elapsed / duration;
            a.transform.position = Vector3.Lerp(fromA, fromB, t);
            b.transform.position = Vector3.Lerp(fromB, fromA, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void FinishSwap()
    {
        _isSwapping = false;
        _matchTiles.SetSwappingState(false);
        _firstClickTile = null;
        _pointerTile    = null;
        _dragStarted    = false;
        _swapQueued     = false;
    }
    
    private GameObject GetTileAtScreenPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("Tile"));
        foreach (RaycastHit hit in hits)
            if (hit.collider != null && hit.collider.GetComponent<Tile>() != null)
                return hit.collider.gameObject;
        return null;
    }

    private bool AreAdjacent(GameObject a, GameObject b) =>
        Vector3.Distance(a.transform.position, b.transform.position) < swapDistanceThreshold;

    public void SetInputState(bool state) => _inputDisabled = !state;
}