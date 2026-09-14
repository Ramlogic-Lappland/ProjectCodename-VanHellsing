using UnityEngine;
using System;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth =  100f;
    public event Action<float> OnHealthChanged;
    
    private float _health;
    
    private void Start()
    {
        _health = maxHealth;
    }

    private void Update()
    {
        //Solo para tes es esto
        if (Input.GetKeyDown(KeyCode.Q))
        {
           TakeDamage(10); 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(10f);
        }
    }
    
    private void TakeDamage(float damage)
    {
        _health = Mathf.Max(0f, _health - damage);
        OnHealthChanged?.Invoke(_health);
    }

    private void Heal(float heal)
    {
        _health = Mathf.Min(_health + heal, maxHealth);
        OnHealthChanged?.Invoke(_health);
    }
    
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    
}
