using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesRemainingText;
    [SerializeField] private TextMeshProUGUI stageClearText;
    [SerializeField] private Light2D globalLight; 
    [SerializeField] private float globalLightIntensity = 0.15f;
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

        WindowBehaviour[] windows = FindObjectsByType<WindowBehaviour>(FindObjectsSortMode.None);
        globalLight.intensity = globalLightIntensity;
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