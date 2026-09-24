using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UIPause : MonoBehaviour
{
    [SerializeField] private Button buttonResume;
    //[SerializeField] private Button buttonSettings;
    //[SerializeField] private Button buttonMainMenu;

    //[SerializeField] private Button buttonSettingsBack;

    [SerializeField] private CanvasGroup panelPause;
    //[SerializeField] private CanvasGroup panelSettings;

    private bool _isPause = false;

    public event Action GameTogglePouse;

    private void Awake()
    {
        buttonResume.onClick.AddListener(OnButtonResumeClicked);
        //buttonSettings.onClick.AddListener(OnButtonSettingsClicked);
        //buttonMainMenu.onClick.AddListener(OnButtonMainMenuClicked);
        //buttonSettingsBack.onClick.AddListener(OnButtonSettingsBackClicked);
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        
    }

    private void OnDestroy()
    {
        buttonResume.onClick.RemoveAllListeners();
        //buttonSettings.onClick.RemoveAllListeners();
        //buttonMainMenu.onClick.RemoveAllListeners();
      
    }

    private void OnButtonResumeClicked()
    {
        TogglePause();
    }
    /*private void OnButtonSettingsClicked()
    {
        SetPanel(panelPause, false);
        SetPanel(panelSettings, true);
    }
    private void OnButtonMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    private void OnButtonSettingsBackClicked()
    {
        SetPanel(panelPause, true);
        SetPanel(panelSettings, false);
    }*/

    private void SetPanel(CanvasGroup panel, bool state)
    {
        panel.alpha = state ? 1 : 0;
        panel.interactable = state;
        panel.blocksRaycasts = state;
    }
    void TogglePause()
    {
        _isPause = !_isPause;
        Time.timeScale = _isPause ? 0 : 1;
        SetPanel(panelPause, _isPause);
        GameTogglePouse?.Invoke();
        //Cursor.lockState = _isPause ? CursorLockMode.None : CursorLockMode.Locked;
        //Cursor.visible = _isPause;
    }

}
