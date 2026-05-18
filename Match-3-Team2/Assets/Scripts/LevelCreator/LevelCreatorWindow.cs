using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCreatorWindow : EditorWindow
{
    #region Fields

    private LevelConfig currentConfig;
    private Vector2 scrollPosition;
    private List<string> existingLevels = new List<string>();

    // Prefab references
    private GameObject gridSystemPrefab;
    private GameObject uiPrefab;
    private GameObject playerPrefab;
    private GameObject enemiesPrefab;

    #endregion

    #region Constants

    private const string LEVEL_PATH = "Assets/Scenes/Levels";
    private const string LEVEL_SCENE_EXTENSION = "*.unity";

    #endregion

    #region Menu Items

    [MenuItem("Window/Level Creator")]
    public static void ShowWindow()
    {
        GetWindow<LevelCreatorWindow>("Level Creator");
    }

    #endregion

    #region Lifecycle

    private void OnEnable()
    {
        currentConfig = new LevelConfig("NewLevel");
        RefreshLevelList();
    }

    #endregion

    #region GUI

    private void OnGUI()
    {
        DrawHeader();
        DrawPrefabReferences();
        DrawLevelConfiguration();
        DrawCreateButton();
        DrawExistingLevels();
    }

    private void DrawHeader()
    {
        GUILayout.Label("Level Creator", EditorStyles.boldLabel);
        EditorGUILayout.Space();
    }

    private void DrawPrefabReferences()
    {
        GUILayout.Label("Prefab References", EditorStyles.boldLabel);
        gridSystemPrefab = EditorGUILayout.ObjectField("Grid System Prefab", gridSystemPrefab, typeof(GameObject), false) as GameObject;
        uiPrefab = EditorGUILayout.ObjectField("UI Prefab", uiPrefab, typeof(GameObject), false) as GameObject;
        playerPrefab = EditorGUILayout.ObjectField("Player Prefab", playerPrefab, typeof(GameObject), false) as GameObject;
        enemiesPrefab = EditorGUILayout.ObjectField("Enemies Prefab", enemiesPrefab, typeof(GameObject), false) as GameObject;
        EditorGUILayout.Space();
    }

    private void DrawLevelConfiguration()
    {
        GUILayout.Label("Create New Level", EditorStyles.boldLabel);
        currentConfig.levelName = EditorGUILayout.TextField("Level Name", currentConfig.levelName);

        EditorGUILayout.Space();
        GUILayout.Label("Scene Components", EditorStyles.boldLabel);

        currentConfig.includeGridSystem = EditorGUILayout.Toggle("Grid System", currentConfig.includeGridSystem);
        if (currentConfig.includeGridSystem)
        {
            currentConfig.gridSize = EditorGUILayout.Vector2IntField("Grid Size", currentConfig.gridSize);
        }

        currentConfig.includeUI = EditorGUILayout.Toggle("UI", currentConfig.includeUI);
        currentConfig.includePlayer = EditorGUILayout.Toggle("Player", currentConfig.includePlayer);
        currentConfig.includeEnemies = EditorGUILayout.Toggle("Enemies", currentConfig.includeEnemies);
        EditorGUILayout.Space();
    }

    private void DrawCreateButton()
    {
        if (GUILayout.Button("Create Level Scene", GUILayout.Height(30)))
        {
            CreateLevelScene();
            RefreshLevelList();
        }

        EditorGUILayout.Space();
    }

    private void DrawExistingLevels()
    {
        GUILayout.Label("Existing Levels", EditorStyles.boldLabel);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        if (existingLevels.Count == 0)
        {
            EditorGUILayout.HelpBox("No levels found in Assets/Scenes/Levels folder", MessageType.Info);
        }
        else
        {
            foreach (var level in existingLevels)
            {
                DrawLevelItem(level);
            }
        }

        GUILayout.EndScrollView();
    }

    private void DrawLevelItem(string levelName)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(levelName);

        if (GUILayout.Button("Open", GUILayout.Width(50)))
        {
            EditorSceneManager.OpenScene($"{LEVEL_PATH}/{levelName}.unity", OpenSceneMode.Single);
        }

        EditorGUILayout.EndHorizontal();
    }

    #endregion

    #region Level Creation

    private void CreateLevelScene()
    {
        if (!ValidateLevelCreation())
        {
            return;
        }

        string scenePath = GetScenePath();
        Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        InstantiateSelectedComponents();

        EditorSceneManager.SaveScene(newScene, scenePath);
        EditorUtility.DisplayDialog("Success", $"Level '{currentConfig.levelName}' created at {scenePath}", "OK");
    }

    private bool ValidateLevelCreation()
    {
        if (string.IsNullOrEmpty(currentConfig.levelName))
        {
            EditorUtility.DisplayDialog("Error", "Please enter a level name", "OK");
            return false;
        }

        if (currentConfig.includeGridSystem && gridSystemPrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "Grid System prefab not assigned", "OK");
            return false;
        }

        if (currentConfig.includeUI && uiPrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "UI prefab not assigned", "OK");
            return false;
        }

        if (currentConfig.includePlayer && playerPrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "Player prefab not assigned", "OK");
            return false;
        }

        if (currentConfig.includeEnemies && enemiesPrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "Enemies prefab not assigned", "OK");
            return false;
        }

        return true;
    }

    private string GetScenePath()
    {
        if (!Directory.Exists(LEVEL_PATH))
        {
            Directory.CreateDirectory(LEVEL_PATH);
        }

        string scenePath = $"{LEVEL_PATH}/{currentConfig.levelName}.unity";

        if (File.Exists(scenePath))
        {
            EditorUtility.DisplayDialog("Error", "Level already exists", "OK");
            throw new System.InvalidOperationException("Scene already exists");
        }

        return scenePath;
    }

    private void InstantiateSelectedComponents()
    {
        if (currentConfig.includeGridSystem)
        {
            CreateGridSystem();
        }

        if (currentConfig.includeUI)
        {
            CreateUI();
        }

        if (currentConfig.includePlayer)
        {
            CreatePlayer();
        }

        if (currentConfig.includeEnemies)
        {
            CreateEnemies();
        }
    }

    #endregion

    #region Component Creation

    private void CreateGridSystem()
    {
        if (gridSystemPrefab != null)
        {
            PrefabUtility.InstantiatePrefab(gridSystemPrefab);
        }
        else
        {
            Debug.LogError("Grid System prefab is not assigned");
        }
    }

    private void CreateUI()
    {
        if (uiPrefab != null)
        {
            PrefabUtility.InstantiatePrefab(uiPrefab);
        }
        else
        {
            Debug.LogError("UI prefab is not assigned");
        }
    }

    private void CreatePlayer()
    {
        if (playerPrefab != null)
        {
            PrefabUtility.InstantiatePrefab(playerPrefab);
        }
        else
        {
            Debug.LogError("Player prefab is not assigned");
        }
    }

    private void CreateEnemies()
    {
        if (enemiesPrefab != null)
        {
            PrefabUtility.InstantiatePrefab(enemiesPrefab);
        }
        else
        {
            Debug.LogError("Enemies prefab is not assigned");
        }
    }

    #endregion

    #region Utilities

    private void RefreshLevelList()
    {
        existingLevels.Clear();

        if (!Directory.Exists(LEVEL_PATH))
        {
            return;
        }

        string[] sceneFiles = Directory.GetFiles(LEVEL_PATH, LEVEL_SCENE_EXTENSION);
        foreach (var file in sceneFiles)
        {
            string levelName = Path.GetFileNameWithoutExtension(file);
            existingLevels.Add(levelName);
        }
    }

    #endregion
}
