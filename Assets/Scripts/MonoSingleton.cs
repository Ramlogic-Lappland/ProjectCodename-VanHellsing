using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    [SerializeField] bool dontDestroyOnLoad = true;

    public static T Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (_instance == null || _instance == this)
        {
            _instance = this as T;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            OnAwaken();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
            OnDestroyed();
        }
    }

    protected virtual void OnAwaken()
    {

    }

    protected virtual void OnDestroyed()
    {

    }
}