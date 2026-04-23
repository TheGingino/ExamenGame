using UnityEngine;
using Random = UnityEngine.Random;

//[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileSO tileData;
    private bool _canBeSwapped = true;
    
    public TileSO _tileData => tileData;
    
     public void SetType(TileSO newTileData)
     {
         tileData = newTileData;
         SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
         if (spriteRenderer != null && tileData.tileSprite != null) 
         {
             spriteRenderer.sprite = tileData.tileSprite;
         }
     }
    private void Start()
    {
        if (tileData == null)
        {
            //Debug.LogWarning($"Tile '{name}' has no TileSO assigned.");
            return;
        }
    }
    
    public void DestroyTile() => Destroy(gameObject);
}