using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Weapon weapon;
    
    
    void Start()
    {
        
    }
    
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleShooting();
    }
    
    private void HandleMovement()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        transform.position += (Vector3)(speed * movement * Time.deltaTime);
    }
    
    private void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 distance;
        distance.x = mousePosition.x - transform.position.x;
        distance.y = mousePosition.y - transform.position.y;

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
}
