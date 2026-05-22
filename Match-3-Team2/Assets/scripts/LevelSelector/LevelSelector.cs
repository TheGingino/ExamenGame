using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private string _buttonID;
    [SerializeField] private Scene _scene;

    public void LevelSelect()
    {
        switch (_buttonID)
        {
            case "lvl1":
                break;
            case "lvl2":
                break;
            case "lvl3":
                break;
            case "lvl4":
                SceneManager.LoadScene("FinalLevel");
                break;
            case "menu":
                SceneManager.LoadScene("StartScreen");
                break;
        }
    }
}