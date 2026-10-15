using UnityEngine;

public class TestSound : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event TestEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AkUnitySoundEngine.PostEvent(TestEvent.Id, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
