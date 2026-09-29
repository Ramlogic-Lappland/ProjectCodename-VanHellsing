using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WindowBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite windowNormalSprite;
    [SerializeField] private Sprite windowLevelClearSprite;
    [SerializeField] private Light2D windowLight;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        windowLight = GetComponentInChildren<Light2D>();
    }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = windowNormalSprite;
    }

    public void ClearWindow()
    {
        _spriteRenderer.sprite = windowLevelClearSprite;
        windowLight.lightCookieSprite = windowLevelClearSprite;
    }
}