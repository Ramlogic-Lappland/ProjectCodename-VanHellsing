using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
