using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Match3/Tile")]
public class TileSO : ScriptableObject
{
    public Sprite tileSprite;
    public TileType tileType;
    
}
public enum TileType
{
    RED,
    GREEN,
    BLUE,
    YELLOW,
    PURPLE
}