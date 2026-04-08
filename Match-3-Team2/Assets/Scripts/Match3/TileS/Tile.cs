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