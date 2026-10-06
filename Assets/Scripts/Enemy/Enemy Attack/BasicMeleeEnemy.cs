using UnityEngine;

public class BasicMeleeEnemy : MonoBehaviour, IEnemyAttack
{
    [Header("Melee")]
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackCooldown = 1f;

    private float _cooldownTimer;
    private PlayerHealth _playerHealth;

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

        float distance = Vector2.Distance(transform.position, player.position);

        return distance <= AttackRange;
    }

    public void Attack(Transform player)
    {
        if (player == null)
        {
            return;
        }

        if (_cooldownTimer > 0f)
        {
            return;
        }

        // Find PlayerHealth once.
        if (_playerHealth == null)
        {
            _playerHealth =
                player.GetComponentInParent<PlayerHealth>();
        }

        if (_playerHealth == null)
        {
            Debug.LogError("BasicMeleeEnemy could not find PlayerHealth on the player.");

            return;
        }

        _cooldownTimer = attackCooldown;

        Debug.Log($"Melee attack! Damage: {damage}");

        _playerHealth.TakeDamage(damage);
    }
}