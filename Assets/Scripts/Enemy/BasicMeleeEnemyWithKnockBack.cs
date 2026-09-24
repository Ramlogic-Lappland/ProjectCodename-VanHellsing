using UnityEngine;

public class BasicMeleeEnemyWithKnockBack : MonoBehaviour, IEnemyAttack
{
    [Header("Melee")]
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 5f;

    private float _cooldownTimer;
    private PlayerHealth _playerHealth;
    private Rigidbody2D _playerRb;

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

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

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

        // Cache PlayerHealth and Rigidbody2D.
        if (_playerHealth == null)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
        }

        if (_playerRb == null)
        {
            _playerRb = player.GetComponent<Rigidbody2D>();
        }

        if (_playerHealth == null)
        {
            Debug.LogWarning("Player does not have PlayerHealth.");
            return;
        }

        if (_playerRb == null)
        {
            Debug.LogWarning("Player does not have Rigidbody2D.");
            return;
        }

        _cooldownTimer = attackCooldown;

        Debug.Log("Melee attack!");
        
        _playerHealth.TakeDamage(damage);

        // Push player away from enemy.
        Vector2 knockbackDirection = ((Vector2)player.position - (Vector2)transform.position).normalized;

        _playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }
}