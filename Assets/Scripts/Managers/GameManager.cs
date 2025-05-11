using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnPlayerLose += OnLose;
        GameEvents.OnPlayerWin += OnWin;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerLose -= OnLose;
        GameEvents.OnPlayerWin -= OnWin;
    }

    private void OnLose()
    {
        Debug.Log("Game Over!");
    }

    private void OnWin()
    {
        Debug.Log("Victory!");
    }
}
