using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private Transform muzzle;
    private FireBehaviour _fireBehaviour;


    private void Awake()
    {
        switch (data.GetFireType())
        {
            case FireType.Automatic:
                _fireBehaviour = gameObject.AddComponent<AutomaticFire>();
                break;
        }
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
        _fireBehaviour.Shoot(this);
    }
}
