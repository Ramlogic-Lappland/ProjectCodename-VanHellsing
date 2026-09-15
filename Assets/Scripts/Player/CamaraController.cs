using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float orthographicSize = 8f;
    private Transform _fow;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = orthographicSize;
        float height = _camera.orthographicSize * 2f;
        float width = height * _camera.aspect;
        
        _fow = transform.GetChild(0);
        _fow.localScale =  Vector3.one;
        _fow.localPosition = Vector3.zero;
        if (_fow != null)
        {
            _fow.localScale = new Vector3(width, height, 1f);
        }
    }
    
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
