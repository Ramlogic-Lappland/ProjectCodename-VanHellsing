using UnityEngine;

public class BasicMeleeEnemy : MonoBehaviour, IEnemyAttack
{
    [Header("Melee")]
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackCooldown = 1f;

    private float _cooldownTimer;
    
    public float AttackRange => attackRange;

    private void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
    
    public bool CanStartAttack(Transform player)
    {
        if (player == null)
        {
            return false;
        }

        if (_cooldownTimer > 0f)
        {
            return false;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        return distance <= AttackRange;
    }
    
    public void Attack()
    {
        _cooldownTimer = attackCooldown;

        Debug.Log("Melee attack!");

        // TODO:Deal damage to player
    }
}