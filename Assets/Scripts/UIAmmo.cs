using UnityEngine;
using TMPro;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;

    public void UpdateUIAmmo(int currentAmmo, int maxAmmo)
    {
        ammoText.text = $"{currentAmmo} / {maxAmmo}";
    }
}
