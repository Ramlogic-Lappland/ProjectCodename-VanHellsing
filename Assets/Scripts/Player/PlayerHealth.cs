using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth =  100f;
    
    private float _health;
    
    void Start()
    {
        _health = maxHealth;
    }
    
    private void GetDamage(float damage)
    {
        _health -= damage;
    }

    private void Heal(float heal)
    {
        _health += heal;
    }
}
