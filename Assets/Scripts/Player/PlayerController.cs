using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Weapon weapon;
    
    [SerializeField] private UIPause uiPause;
    private bool _gamePasued;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gamePasued = false;
        uiPause.GameTogglePouse += toggleFreezeInput;
    }
    
    private void Update()
    {
        if (_gamePasued)
            return;
        
        HandleRotation();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnDisable()
    {
        uiPause.GameTogglePouse -= toggleFreezeInput;
    }

    private void HandleMovement()
    {
        Vector2 movement = new Vector2
        (
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
        
        Vector2 targetPosition = _rb.position + movement * (speed * Time.fixedDeltaTime) ;
        _rb.MovePosition(targetPosition);
    }
    
    private void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 distance = mousePosition - transform.position;

        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle -90);
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
