using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private List<Image> bullets;

    private void Awake()
    {
        reloadSlider.gameObject.SetActive(false);
    }
    
    public void UpdateUIAmmo(int currentAmmo, int maxAmmo)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].gameObject.SetActive(i < currentAmmo);
        }
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
