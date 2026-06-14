using UnityEngine;

public enum GameState
{
    Playing, 
    GameOver, 
    Win
}

public class GameManager
{
    public GameState State { get; private set; }
    public EntityObject Player { get; private set; }

    public void SetState(GameState newState)
    {
        State = newState;

        if (State == GameState.GameOver)
        {
            Debug.Log("Game Over");
        }
        if (State == GameState.Win)
        {
            Debug.Log("You win");
        }
    }

    public void SetPlayer(EntityObject player)
    {
        Player = player;
    }
}
