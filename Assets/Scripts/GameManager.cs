using Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _gameState;

    private void Awake()
    {
        _gameState = GameState.Pause;
    }

    public GameState GetCurrentState()
    {
        return _gameState;
    }
    public void SetGameState(GameState state)
    {
        _gameState = state;
    }
}
