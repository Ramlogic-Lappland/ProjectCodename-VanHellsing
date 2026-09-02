using UnityEngine;

public enum FireType
{
    Automatic
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float fireRate; //Disparos por minuto
    [SerializeField] private float range;
    
    [SerializeField] private Bullet projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private FireType fireType;

    public Bullet GetProjectile()
    {
        return projectilePrefab;
    }

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }
    
    public float GetDamage()
    {
        return damage;
    }

    public float FireRate()
    {
        return fireRate;
    }

    public FireType GetFireType()
    {
        return fireType;
    }
}
