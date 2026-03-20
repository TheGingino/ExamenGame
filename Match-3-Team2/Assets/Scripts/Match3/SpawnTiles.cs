using System.Collections;
using UnityEngine;
public class SpawnTiles : MonoBehaviour
{
    [SerializeField] private Tile[] tilePrefabs;
    [SerializeField] private float spawnDelay = 0.5f;

    GridSystem gridSystem;
    private Transform gridTransform;

    private void Start()
    {
        gridSystem = GetComponent<GridSystem>();
        gridTransform = transform;
        SpawnInitialTiles();
    }

    private void SpawnInitialTiles()
    {
        for (int x = 0; x < gridSystem.width; x++)
        {
            for (int y = 0; y < gridSystem.height; y++)
            {
                SpawnTile(x, y);
            }
        }
        
    }

    private void SpawnTile(int x, int y)
    {
        //needs to check if there is a match connected to the tile before spawning a new one, if there is a match then it needs to spawn a different tile
        //SpawnTileWithNoMatch(x,y);
    }
    
    public IEnumerator SpawnTileWithDelay(int x, int y)
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnTile(x, y);
    }

    /*private void SpawnTiless(int x, int y)
    {
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        Tile tilePrefab = tilePrefabs[randomIndex];
        Vector2 spawnPosition = gridSystem.GetWorldPosition(x, y);
        
        GameObject newTile = Instantiate(tilePrefab.gameObject, spawnPosition, Quaternion.identity, gridTransform);
        newTile.name = $"Tile_{x}_{y}";
    }*/
    
    private void SpawnTileWithNoMatch(int x, int y)
    {
            int randomIndex = Random.Range(0, tilePrefabs.Length);
            Tile tilePrefab = tilePrefabs[randomIndex];
            Vector2 spawnPosition = gridSystem.GetWorldPosition(x, y);
            
            GameObject newTile = Instantiate(tilePrefab.gameObject, spawnPosition, Quaternion.identity, gridTransform);
            newTile.name = $"Tile_{x}_{y}";
            foreach (Tile VARIABLE in gridTransform.GetComponentsInChildren<Tile>())
            {
                if (VARIABLE.transform.position == new Vector3(spawnPosition.x, spawnPosition.y + gridSystem.cellSize, 0) ||
                    VARIABLE.transform.position == new Vector3(spawnPosition.x + gridSystem.cellSize, spawnPosition.y, 0))
                {
                    if (VARIABLE._tileData.tileType == tilePrefab._tileData.tileType)
                    {
                        Destroy(newTile);
                        SpawnTileWithNoMatch(x, y);
                        return;
                    }
                }
            }
    }
}
