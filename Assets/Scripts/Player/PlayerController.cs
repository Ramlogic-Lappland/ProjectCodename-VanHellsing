using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 7.0f;

    [Header("Knockback")]
    [SerializeField] private float knockbackRecovery = 10f;

    [Header("References")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private UIPause uiPause;

    private Rigidbody2D _rb;

    private bool _gamePasued;

    
    private Vector2 _movementInput; // Input movement stored from Update()

    
    private Vector2 _knockbackVelocity; // knockback vel

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _gamePasued = false;

        uiPause.GameTogglePouse += toggleFreezeInput;
    }

    private void Update()
    {
        if (_gamePasued)
        {
            _movementInput = Vector2.zero;
            return;
        }

        HandleInput();
        HandleRotation();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        if (_gamePasued)
        {
            _rb.linearVelocity = Vector2.zero;
            _knockbackVelocity = Vector2.zero;
            return;
        }

        HandleMovement();
        HandleKnockback();
    }

    private void OnDisable()
    {
        uiPause.GameTogglePouse -= toggleFreezeInput;
    }

    private void HandleInput()
    {
        _movementInput = new Vector2
        (
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    private void HandleMovement()
    {
        Vector2 movementVelocity = _movementInput * speed;

        _rb.linearVelocity = movementVelocity + _knockbackVelocity;
    }

    private void HandleKnockback()
    {
        _knockbackVelocity = Vector2.MoveTowards
        (
            _knockbackVelocity,
            Vector2.zero,
            knockbackRecovery * Time.fixedDeltaTime
        );
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (direction.sqrMagnitude <= 0.01f)
        {
            return;
        }

        _knockbackVelocity += direction.normalized * force;
    }

    private void HandleRotation()
    {
        Vector3 mousePosition =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 distance =
            mousePosition - transform.position;

        float angle =
            Mathf.Atan2(distance.y, distance.x) *
            Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler
            (
                0f,
                0f,
                angle - 90f
            );
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.Shoot();
        }
    }

    private void toggleFreezeInput()
    {
        _gamePasued = !_gamePasued;
    }
}