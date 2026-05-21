using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Match3/Tile")]
public class TileSO : ScriptableObject
{
    [Header("Combat Values")]
    public int HealAmount;
    public int DamageAmount;
    public int ShieldAmount;
    public int specialAttackAmount;

    [Header("Sprite and Tiletype")]
    public Sprite tileSprite;
    public TileType tileType;

    private void OnEnable()
    {
        if (tileSprite == null)
        {
            Debug.LogWarning("TileSO '{name}' has no sprite assigned.");
        }
        if (!Enum.IsDefined(typeof(TileType), tileType))
        {
            Debug.LogWarning("TileSO '{name}' has an invalid TileType assigned.");
        }
    }
}

public enum TileType
{
    Normal,
    Heal,
    Shield,
    Damage,
    Special,
}