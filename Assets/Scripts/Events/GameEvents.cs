using System;

public static class GameEvents
{
    public static event Action OnPlayerLose;
    public static event Action OnPlayerWin;

    public static void PlayerLose() => OnPlayerLose?.Invoke();
    public static void PlayerWin() => OnPlayerWin?.Invoke();
}