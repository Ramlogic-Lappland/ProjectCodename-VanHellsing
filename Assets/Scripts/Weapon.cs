using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private Transform muzzle;
    private FireBehaviour _fireBehaviour;
    private float _reloadTimer;
    private float _roundsInMag;
    private bool _canShoot;
    
    [SerializeField] private UIAmmo uiAmmo;
    
    private void Awake()
    {
        switch (data.GetFireType())
        {
            case FireType.Automatic:
                _fireBehaviour = gameObject.AddComponent<AutomaticFire>();
                break;
        }
    }

    private void Start()
    {
        _roundsInMag = data.GetMagazineSize();
        _canShoot = true;
        uiAmmo.UpdateUIAmmo((int)_roundsInMag, (int)data.GetMagazineSize());
    }

    private void Update()
    {
        HandleReload();
    }

    public WeaponData GetData()
    {
        return data;
    }

    
    public Transform GetMuzzle()
    {
        return muzzle;
    }

    public void Shoot()
    {
        if (_canShoot)
        {
            _fireBehaviour.Shoot(this);
            uiAmmo.UpdateUIAmmo((int)_roundsInMag,(int)data.GetMagazineSize());
        }
    }

    private void HandleReload()
    {
        if (_roundsInMag > 0)
            return;
        
        _canShoot = false;
        _reloadTimer += Time.deltaTime;
        uiAmmo.SetReload(_reloadTimer, data.GetReloadTime());
        
        if (_reloadTimer >= data.GetReloadTime())
        {
            Reload();
            uiAmmo.ResetReload();
        }
        
    }

    private void Reload()
    {
        _roundsInMag = data.GetMagazineSize();
        _reloadTimer = 0f;
        _canShoot = true;
        uiAmmo.UpdateUIAmmo((int)_roundsInMag, (int)data.GetMagazineSize());
    }
    
    public void DecreaseRounds()
    {
        _roundsInMag--;
    }
}
