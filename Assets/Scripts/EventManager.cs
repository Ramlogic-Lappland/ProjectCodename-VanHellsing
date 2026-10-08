using UnityEngine;
using System;
public class EventManager : MonoSingleton<EventManager>
{
    
    public event Action OnCursorChange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCursor()
    {
        OnCursorChange?.Invoke();
    }
}
