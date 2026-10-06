using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesRemainingText;
    [SerializeField] private TextMeshProUGUI stageClearText;
    [SerializeField] private GameObject exitPoint;
    [SerializeField] private Light2D globalLight; 
    [SerializeField] private Light2D outsideLight;
    [SerializeField] private float globalLightIntensity = 0.15f;
    public string hexColor = "#EFA35B";
    private int _remainingEnemies;
    private bool _gameEnded;
    
    private void Start()
    {
        _remainingEnemies = FindObjectsByType<BasicEnemyBehaviour>(FindObjectsSortMode.None).Length;
        if (globalLight == null)
        {
            globalLight = GetComponent<Light2D>();
        }
        UpdateEnemyCounter();
        CheckWinCondition();
    }

    public void EnemyDefeated()
    {
        _remainingEnemies--;

        UpdateEnemyCounter();
        CheckWinCondition();
    }

    private void UpdateEnemyCounter()
    {
        
            enemiesRemainingText.text = $"Enemies Remaining: {_remainingEnemies}";
            
    }

    private void CheckWinCondition()
    {
        if (_remainingEnemies <= 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("YOU WIN!");
        stageClearText.gameObject.SetActive(true);
        exitPoint.SetActive(true);

        WindowBehaviour[] windows = FindObjectsByType<WindowBehaviour>(FindObjectsSortMode.None);
        globalLight.intensity = globalLightIntensity;
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            if (outsideLight != null)
            {
                outsideLight.color = newColor;
            }
        }
        foreach (WindowBehaviour window in windows)
        {
            window.ClearWindow();
        }
        
        // TODO: Show win screen
    }
    
    public void PlayerDefeated()
    {
        if (_gameEnded)
            return;

        _gameEnded = true;
        
        Debug.Log("YOU LOSE!");

        // TODO: Show lose screen, Disable player, Stop game.
    }
}