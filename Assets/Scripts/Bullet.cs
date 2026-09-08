using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    
    
    
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.position += (transform.up * _speed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
