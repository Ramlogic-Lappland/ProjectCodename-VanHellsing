using TMPro;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesRemainingText;

    private int _remainingEnemies;

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
        
        // TODO: Show win screen, pause gameplay
    }
}