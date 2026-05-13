using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : UsableItem
{
    private TileGravity _tileGravity;
    private GridSystem _gridSystem;
    private MatchTiles _matchTiles;
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector3 _dragOffset;

    [SerializeField] private GameObject originalPosition;
    [SerializeField] private GameObject envCover;
    [SerializeField] private AudioSource bombSFX;
    
    private void Start()
    {
        _tileGravity = FindObjectOfType<TileGravity>();
        _gridSystem = FindObjectOfType<GridSystem>();
        _matchTiles = FindObjectOfType<MatchTiles>();
        _mainCamera = Camera.main;
        
        envCover.SetActive(false);
    }

    public override void Use()
    {
        base.Use();
        Debug.Log(Quantity);

        Vector2Int bombPos = _gridSystem.GetGridPosition(transform.position);

        if (bombPos.x < 0 || bombPos.x >= _gridSystem.width ||
            bombPos.y < 0 || bombPos.y >= _gridSystem.height)
        {
            return;
        }

        Explode(bombPos);
    }

    private void Explode(Vector2Int center)
    {
        bombSFX.Play();
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
        yield return new WaitForSeconds(0.1f);

        SpawnTiles spawnTiles = FindObjectOfType<SpawnTiles>();
        MatchTiles matchTiles = FindObjectOfType<MatchTiles>();

        if (spawnTiles != null)
        {
            for (int x = 0; x < _gridSystem.width; x++)
            {
                int emptyCount = 0;
                for (int y = 0; y < _gridSystem.height; y++)
                {
                    if (_tileGravity.GetTileAt(x, y) == null)
                        emptyCount++;
                }

                if (emptyCount > 0)
                {
                    yield return new WaitForSeconds(0.05f);
                    yield return spawnTiles.FillColumn(x, emptyCount);
                    yield return new WaitForSeconds(0.05f);
                    yield return _tileGravity.ApplyGravityContinuously();
                }
            }
        }

        yield return _tileGravity.ApplyGravityContinuously();

        if (matchTiles != null)
            matchTiles.CheckForMatches();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tile") && Quantity > 0)
        {
            Vector3 hitPoint = other.contacts[0].point;
            Vector2Int gridPos = _gridSystem.GetGridPosition(hitPoint);
            Explode(gridPos);
        }
    }

    private void OnMouseDown()
    {
        if (Quantity <= 0) return;

        _isDragging = true;
        _dragOffset = transform.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        if (Quantity <= 0) return;

        _isDragging = true;
        envCover.SetActive(true);
        transform.position = GetMousePosition() + _dragOffset;
        
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        envCover.SetActive(false);
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
        mousePos.z = 10f;
        return _mainCamera.ScreenToWorldPoint(mousePos);
    }
}
