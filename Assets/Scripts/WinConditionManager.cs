using TMPro;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesRemainingText;

    private int _remainingEnemies;
    private bool _gameEnded;
    
    private void Start()
    {
        _remainingEnemies =
            FindObjectsByType<BasicEnemyBehaviour>(
                FindObjectsSortMode.None
            ).Length;

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
        enemiesRemainingText.text =
            $"Enemies Remaining: {_remainingEnemies}";
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

        WindowBehaviour[] windows =
            FindObjectsByType<WindowBehaviour>(
                FindObjectsSortMode.None
            );

        foreach (WindowBehaviour window in windows)
        {
            window.ClearWindow();
        }
        
        // TODO: Show win screen, Disable player, Pause gameplay
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