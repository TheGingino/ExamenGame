using UnityEngine;

public class MenuSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _container;
    [SerializeField] private RectTransform _startScreen;
    [SerializeField] private RectTransform _options;

    [Header("Settings")]
    [SerializeField] private float _smoothTime = 0.2f;

    private Vector2 _targetPos;
    private Vector2 _velocity;

    private void Start()
    {
        SetupPanels();
        _targetPos = _container.anchoredPosition;
    }

    private void Update()
    {
        _container.anchoredPosition = Vector2.SmoothDamp(
            _container.anchoredPosition,
            _targetPos,
            ref _velocity,
            _smoothTime
        );
    }

    private void SetupPanels()
    {
        float width = _startScreen.rect.width;

        // StartScreen stays at center
        _startScreen.anchoredPosition = Vector2.zero;

        // Options sits exactly to the right
        _options.anchoredPosition = new Vector2(width, 0);
    }

    public void OpenOptions()
    {
        float width = _startScreen.rect.width;
        _targetPos = new Vector2(-width, 0);
    }
    
    

    public void BackToMain()
    {
        _targetPos = Vector2.zero;
    }
}