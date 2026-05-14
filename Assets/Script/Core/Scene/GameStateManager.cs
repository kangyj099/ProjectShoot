using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : StateMachine<GameState, GameStateManager>
{
    public GameStateManager() : base(null) { }

    public void Init()
    {
        states[GameState.MainMenu] = new MainState();
        states[GameState.Loading] = new LoadingState();
        states[GameState.Playing] = new PlayingState();
        states[GameState.Pause] = new PauseState();

        // 처음 상태는 메인임
        ChangeState(GameState.MainMenu);
    }
}