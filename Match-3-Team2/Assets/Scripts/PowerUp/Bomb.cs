using System.Collections;
using UnityEngine;

public class Bomb : UsableItem
{
    private TileGravity _tileGravity;
    private GridSystem _gridSystem;
    private MatchTiles _matchTiles;
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector3 _dragOffset;
    
    private void Start()
    {
        _tileGravity = FindObjectOfType<TileGravity>();
        _gridSystem = FindObjectOfType<GridSystem>();
        _matchTiles = FindObjectOfType<MatchTiles>();
        _mainCamera = Camera.main;
    }

    public override void Use()
    {
        base.Use();
        Debug.Log(Quantity);

        // Get bomb's grid position
        Vector2Int bombPos = _gridSystem.GetGridPosition(transform.position);
    
        // Check if bomb is on the grid
        if (bombPos.x < 0 || bombPos.x >= _gridSystem.width || 
            bombPos.y < 0 || bombPos.y >= _gridSystem.height)
        {
            return; // Bomb is not on grid, don't explode
        }
    
        Explode(bombPos);
    }

    private void Explode(Vector2Int center)
    {
        for (int x = center.x - 1; x <= center.x + 1; x++)
        {
            for (int y = center.y - 1; y <= center.y + 1; y++)
            {
                if (x < 0 || x >= _gridSystem.width || y < 0 || y >= _gridSystem.height)
                    continue;

                Tile tile = _tileGravity.GetTileAt(x, y);
                if (tile != null)
                {
                    tile.DestroyTile();
                    _tileGravity.RegisterTile(x, y, null);
                }
            }
        }
        StartCoroutine(RefillAfterExplosion());
    }

    private IEnumerator RefillAfterExplosion()
    {
        yield return _tileGravity.ApplyGravityContinuously();

        SpawnTiles spawnTiles = FindObjectOfType<SpawnTiles>();
        if (spawnTiles != null)
        {
            for (int x = 0; x < _gridSystem.width; x++)
            {
                int empties = 0;
                for (int y = 0; y < _gridSystem.height; y++)
                {
                    if (_tileGravity.GetTileAt(x, y) == null) empties++;
                }

                if (empties > 0)
                    yield return spawnTiles.FillColumn(x, empties);
            }
        }
        yield return _tileGravity.ApplyGravityContinuously();
        _matchTiles.CheckForMatches();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Only explode on collision if bomb is on grid and hasn't been used yet
        if (other.gameObject.CompareTag("Tile") && Quantity > 0)
        {
            Vector3 hitPoint = other.contacts[0].point;
            Vector2Int gridPos = _gridSystem.GetGridPosition(hitPoint);
            Explode(gridPos);
        }
    }

    private Vector3 offset;
    
    [SerializeField] private GameObject originalPosition;
    
    private void OnMouseDown()
    {
        if (Quantity <= 0) return;
    
        _isDragging = true;
        offset = transform.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        if (Quantity <= 0) return;
    
        _isDragging = true;
        transform.position = GetMousePosition() + offset;
    }

    private void OnMouseUp()
    {
        _isDragging = false;

        if (Quantity <= 0) return;

        Vector2Int bombPos = _gridSystem.GetGridPosition(transform.position);
        if (bombPos.x >= 0 && bombPos.x < _gridSystem.width &&
            bombPos.y >= 0 && bombPos.y < _gridSystem.height)
        {
            Use();
        }

        StartCoroutine(ReturnToOriginalPosition());
    }
    
    private IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.position = originalPosition.transform.position;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Set this to the distance from the camera to the object
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
