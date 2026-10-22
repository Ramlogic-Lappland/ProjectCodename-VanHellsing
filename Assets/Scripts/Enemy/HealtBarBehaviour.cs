using System;
using UnityEngine;
using UnityEngine.UI;

public class HealtBarBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color low = Color.red;
    [SerializeField] private Color high = Color.green;
    [SerializeField] private Vector3 offSet;

    private Transform _enemy;

    private void Awake()
    {
        _enemy = transform.parent;
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = health;

        slider.gameObject.SetActive(health < maxHealth);

        slider.fillRect.GetComponent<Image>().color = Color.Lerp
        (
            low,
            high,
            slider.normalizedValue
        );
    }

    private void LateUpdate()
    {
        slider.transform.position = _enemy.position + offSet;
        slider.transform.rotation = Quaternion.identity; 
    }
}