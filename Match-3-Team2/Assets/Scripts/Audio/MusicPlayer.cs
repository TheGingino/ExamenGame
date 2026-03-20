using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private AudioSource audioSource;

    [Header("Optional UI")]
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
    }

    private void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = Volume;
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
}