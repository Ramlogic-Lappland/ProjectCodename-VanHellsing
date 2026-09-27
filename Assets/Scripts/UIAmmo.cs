using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Slider reloadSlider;

    private void Awake()
    {
        reloadSlider.gameObject.SetActive(false);
    }
    
    public void UpdateUIAmmo(int currentAmmo, int maxAmmo)
    {
        ammoText.text = $"{currentAmmo} / {maxAmmo}";
    }

    public void SetReload(float currentTime, float totalTime)
    {
        reloadSlider.gameObject.SetActive(true);
        reloadSlider.minValue = 0;
        reloadSlider.maxValue = totalTime;
        reloadSlider.maxValue = totalTime;
        reloadSlider.value = totalTime - currentTime;
    }

    public void ResetReload()
    {
        reloadSlider.value = 0;
        reloadSlider.gameObject.SetActive(false);
    }
}
