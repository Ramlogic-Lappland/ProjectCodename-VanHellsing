using UnityEngine;

public class WindowBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite levelClearSprite;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = normalSprite;
    }

    public void ClearWindow()
    {
        _spriteRenderer.sprite = levelClearSprite;
    }
}