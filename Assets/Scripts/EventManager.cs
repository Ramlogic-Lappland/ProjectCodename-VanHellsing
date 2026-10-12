using UnityEngine;
using System;
public class EventManager : MonoSingleton<EventManager>
{
    
    public event Action OnCursorChange;
    public event Action<int, int, float> UpdateUIAmmoEvent;
    public event Action RestartUIAmmoEvent;
    
    public event Action RestartUIReload;

    public event Action<float, float> StartUIReload;
    
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

    public void UpdateUIAmmo(int  currentAmmo, int maxAmmo, float fireRate)
    {
        UpdateUIAmmoEvent?.Invoke(currentAmmo, maxAmmo, fireRate);
    }

    public void RestartUIAmmo()
    {
        RestartUIAmmoEvent?.Invoke();
    }
    
    public void StartReload(float timer,float  deltaTime)
    {
        StartUIReload?.Invoke(timer,deltaTime);
    }

    public void ResetReload()
    {
        RestartUIReload?.Invoke();
    }
}
