using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileSO tileData;
    //private SpriteRenderer spriteRenderer;
    bool canBeSwapped = true;
    
    public void SetType(TileSO newTileData)
    {
        tileData = newTileData;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && tileData.tileSprite != null)
        {
            spriteRenderer.sprite = tileData.tileSprite;
        }
    }
    public TileSO _tileData => tileData;
    private void Start()
    {
        if (tileData == null)
        {
            Debug.LogWarning($"Tile '{name}' has no TileSO assigned.");
            return;
        }
    }

    public void DestroyTile() => Destroy(gameObject);
    
}