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
        }
    }

    private void HandleReload()
    {
        if (_roundsInMag > 0)
            return;
        
        _canShoot = false;
        _reloadTimer += Time.deltaTime;

        if (_reloadTimer >= data.GetReloadTime())
        {
            Reload();
        }
        
    }

    private void Reload()
    {
        _roundsInMag = data.GetMagazineSize();
        _reloadTimer = 0f;
        _canShoot = true;
    }
    
    public void DecreaseRounds()
    {
        _roundsInMag--;
    }
}
