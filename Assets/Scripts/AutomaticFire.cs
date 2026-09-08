using UnityEngine;

public class AutomaticFire : FireBehaviour
{
    
    void Update()
    {
        UpdateCooldown();
    }
    
    public override void Shoot(Weapon weapon)
    {
        if (!CanFire())
            return;
        
        WeaponData data = weapon.GetData();
        
        Bullet bullet = Instantiate(
            data.GetProjectile(),
            weapon.GetMuzzle().position,
            weapon.GetMuzzle().rotation
        );
        bullet.SetSpeed(data.GetProjectileSpeed());
        SetCooldown(data.FireRate());
    }
}
