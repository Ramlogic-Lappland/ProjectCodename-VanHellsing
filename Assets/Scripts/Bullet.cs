using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float damage = 1f;

    private Rigidbody2D _rb;
    private float _speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _rb.MovePosition(_rb.position + (Vector2)transform.up * _speed * Time.fixedDeltaTime);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BasicEnemyBehaviour enemy =
            collision.collider.GetComponentInParent<BasicEnemyBehaviour>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}