using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private AudioSource audioSource;

    [SerializeField] private Slider volumeSlider;

    public float Volume { get; private set; } = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
        }

        Volume = PlayerPrefs.GetFloat("MusicVolume");
        ApplyVolume();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        volumeSlider = FindFirstObjectByType<Slider>();

        if (volumeSlider != null)
        {
            volumeSlider.value = Volume;

            volumeSlider.onValueChanged.RemoveListener(SetVolume);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
        ApplyVolume();

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    private void ApplyVolume()
    {
        if (audioSource != null)
            audioSource.volume = Volume;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}