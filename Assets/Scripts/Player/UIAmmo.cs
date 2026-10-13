using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private List<Image> bullets;
    [SerializeField] private GameObject cylinder;

    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        reloadSlider.gameObject.SetActive(false);
        _eventManager.UpdateUIAmmoEvent += UpdateUIAmmo;
        _eventManager.RestartUIAmmoEvent += RestartUiAmmo;
        _eventManager.StartUIReload += SetReload;
        _eventManager.RestartUIReload += ResetReload;
    }

    private void OnDisable()
    {
        _eventManager.UpdateUIAmmoEvent -= UpdateUIAmmo;
        _eventManager.RestartUIAmmoEvent -= RestartUiAmmo;
        _eventManager.StartUIReload -= SetReload;
        _eventManager.RestartUIReload -= ResetReload;
    }

    void UpdateUIAmmo(int currentAmmo, int maxAmmo, float fireRate)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].gameObject.SetActive(i >= maxAmmo - currentAmmo);
        }
        float time = 60f / fireRate;
        StartCoroutine(RotateCylinder(time));
    }

    void SetReload(float currentTime, float totalTime)
    {
        reloadSlider.gameObject.SetActive(true);
        reloadSlider.minValue = 0;
        reloadSlider.maxValue = totalTime;
        reloadSlider.maxValue = totalTime;
        reloadSlider.value = totalTime - currentTime;
    }

    void ResetReload()
    {
        reloadSlider.value = 0;
        reloadSlider.gameObject.SetActive(false);
    }

    void RestartUiAmmo()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].gameObject.SetActive(true);
        }
        
        cylinder.transform.Rotate(0, 0, 0);
    }
    
    private IEnumerator RotateCylinder(float duration)
    {
        Quaternion startRotation = cylinder.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 60);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            cylinder.transform.rotation = Quaternion.Slerp(
                startRotation,
                endRotation,
                elapsed / duration
            );

            yield return null;
        }

        cylinder.transform.rotation = endRotation;
    }
}
