using UnityEngine;
using Random = UnityEngine.Random;

//[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileSO tileData;
    private bool _canBeSwapped = true;
    
    [SerializeField] private SpriteRenderer  defaultBackgroundSprite;
    [SerializeField] private Sprite[] backgroundSprites;
    
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
            Debug.LogWarning($"Tile '{name}' has no TileSO assigned.");
            return;
        }
        SetBackground();
    }
    private void SetBackground()
    {
        if (defaultBackgroundSprite == null || backgroundSprites.Length == 0)
        {
            Debug.LogWarning($"Tile '{name}' missing defaultBackgroundSprite or backgroundSprites array is empty.");
            return;
        }

        int randomIndex = Random.Range(0, backgroundSprites.Length);
        defaultBackgroundSprite.sprite = backgroundSprites[randomIndex];
        defaultBackgroundSprite.transform.localScale = Vector3.one;
        Debug.Log($"Tile '{name}' assigned background sprite: {defaultBackgroundSprite.sprite.name}");
    }
    
    public void DestroyTile() => Destroy(gameObject);

    public void Highlight()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow; // Example highlight color
        }
    }


}