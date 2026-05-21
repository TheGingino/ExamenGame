using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileSO _tileData;
    [SerializeField] private VisualEffectAsset _destroyAnimation;
    private bool _canBeSwapped = true;

    public TileSO TileData => _tileData;

    public void SetType(TileSO newTileData)
    {
        _tileData = newTileData;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && _tileData.tileSprite != null)
        {
            spriteRenderer.sprite = _tileData.tileSprite;
        }
    }

    private void Start()
    {
        if (_tileData == null)
        {
            return;
        }
    }

    public IEnumerator PlayAnimationAndDestroy()
    {
        if (_destroyAnimation != null)
        {
            GameObject vfxInstance = new GameObject("DestroyEffect");
            vfxInstance.transform.parent = transform.parent;
            vfxInstance.transform.localScale = Vector3.one * 0.5f;
            vfxInstance.transform.position = transform.position;

            VisualEffect vfx = vfxInstance.AddComponent<VisualEffect>();
            vfx.visualEffectAsset = _destroyAnimation;
            vfx.Play();
            Debug.Log($"Playing destroy animation");
            yield return new WaitForSeconds(0.5f);
            Destroy(vfxInstance);
        }
    }

    public void DestroyTile() => Destroy(gameObject);
}