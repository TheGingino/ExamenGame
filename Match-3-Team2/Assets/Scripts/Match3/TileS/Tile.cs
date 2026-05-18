using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

//[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileSO tileData;
    [SerializeField] private VisualEffectAsset destroyAnimation;
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
    
    public void PlayAnimationAndDestroy()
    {
        if (destroyAnimation != null)
        {
            // Instantiate the visual effect at the tile's position
            GameObject vfxInstance = new GameObject("DestroyEffect");
            vfxInstance.transform.SetParent(transform.parent);
            vfxInstance.transform.localScale = Vector3.one * 0.5f;
            vfxInstance.transform.position = transform.position;

            VisualEffect vfx = vfxInstance.AddComponent<VisualEffect>();
            vfx.visualEffectAsset = destroyAnimation;
            vfx.Play();
        }
    }
    
    public void DestroyTile() => Destroy(gameObject);
}