using UnityEngine;

/// <summary>
/// Configuration data for level creation
/// </summary>
[System.Serializable]
public class LevelConfig
{
    #region Fields

    public string levelName;
    public bool includeGridSystem;
    public bool includeUI;
    public bool includePlayer;
    public bool includeEnemies;
    public Vector2Int gridSize = new Vector2Int(10, 10);

    #endregion

    #region Constructor

    public LevelConfig(string name)
    {
        levelName = name;
    }

    #endregion
}