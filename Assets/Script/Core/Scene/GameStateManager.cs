using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    private Dictionary<GameState, IState> states = new Dictionary<GameState, IState>();
    private IState currentState;

    public void Init()
    {
        states[GameState.MainMenu] = new MainState();
        states[GameState.Loading] = new LoadingState();
        states[GameState.Playing] = new PlayingState();
        states[GameState.Pause] = new PauseState();

        // 처음 상태는 메인임
        ChangeState(GameState.MainMenu);
    }

    public void Release()
    {
        states.Clear();
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log($"상태 머신 동작: {currentState} -> {newState}");

        currentState?.OnExit();
        currentState = states[newState];
        currentState.OnEnter();
    }
}
