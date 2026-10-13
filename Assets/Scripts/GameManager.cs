using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<Texture2D> cursors;
    [SerializeField] private Vector2 hotSpot;
    private Texture2D _cursorTexture;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.Instance.OnCursorChange += ChangeCursor;
        _cursorTexture =  cursors[0];
        Cursor.SetCursor(cursors[0], hotSpot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ChangeCursor()
    {
        if (_cursorTexture == cursors[0])
        {
            Cursor.SetCursor(cursors[1], hotSpot, CursorMode.Auto);
            _cursorTexture = cursors[1];
        }
        else if (_cursorTexture == cursors[1])
        {
            Cursor.SetCursor(cursors[0], hotSpot, CursorMode.Auto);
            _cursorTexture = cursors[0];
        }
        
    }
}
