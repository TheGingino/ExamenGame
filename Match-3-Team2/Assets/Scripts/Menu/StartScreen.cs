using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    //[SerializeField] private AudioSource clickSFX;
    public void PlayGame()
    {
        //clickSFX.Play();
        SceneManager.LoadScene("Main");
    }
    
    public void QuitGame()
    {
        //clickSFX.Play();
        Application.Quit();
    }
}