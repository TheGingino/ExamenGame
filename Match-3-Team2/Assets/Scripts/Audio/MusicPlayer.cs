using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer _instance;
    private AudioSource _audioSource;

    [SerializeField] private Slider _volumeSlider;

    public float Volume { get; private set; } = 1f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();

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
        _volumeSlider = FindFirstObjectByType<Slider>();

        if (_volumeSlider != null)
        {
            _volumeSlider.value = Volume;

            _volumeSlider.onValueChanged.RemoveListener(SetVolume);
            _volumeSlider.onValueChanged.AddListener(SetVolume);
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
        if (_audioSource != null)
            _audioSource.volume = Volume;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}