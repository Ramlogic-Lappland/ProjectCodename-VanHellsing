using UnityEngine;
using UnityEngine.UI;

public class HealtBarBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color low = Color.red;
    [SerializeField] private Color high = Color.green;
    [SerializeField] private Vector3 offSet;

    public void SetHealth(float health, float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = health;

        slider.gameObject.SetActive(health < maxHealth);

        Image fillImage = slider.fillRect.GetComponent<Image>();

        if (fillImage != null)
        {
            fillImage.color = Color.Lerp
            (
                low,
                high,
                slider.normalizedValue
            );
        }
    }

    private void Update()
    {
        slider.transform.position =
            Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
    }
}