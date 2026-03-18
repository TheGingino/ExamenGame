using System.Collections;
using UnityEngine;
public class SpawnTiles : MonoBehaviour
{
    public Tile[] tilePrefabs;
    public float spawnDelay = 0.5f;

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
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        Tile tilePrefab = tilePrefabs[randomIndex];

        Vector3 spawnPosition = gridSystem.GetWorldPosition(x, y);

        Instantiate(tilePrefab, spawnPosition, Quaternion.identity, gridTransform);
    }

    private IEnumerator SpawnTileWithDelay(int x, int y)
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnTile(x, y);
    }
}
