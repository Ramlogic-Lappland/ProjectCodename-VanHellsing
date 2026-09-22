using UnityEngine;

public class PlayerFOV : DynamicFOV
{
    [SerializeField] private float maxRadius = 5f;
    
    private PlayerHealth  _playerHealth;
    

    protected override void Start()
    {
        base.Start();
        _playerHealth = GetComponentInParent<PlayerHealth>();
        _playerHealth.OnHealthChanged += CalculateFOVRadius;
        SetViewRadius(maxRadius);
    }
    

    private void CalculateFOVRadius(float currentHealth)
    {
        SetViewRadius(Mathf.Max(0f,maxRadius *  currentHealth / _playerHealth.GetMaxHealth()));
    }

}