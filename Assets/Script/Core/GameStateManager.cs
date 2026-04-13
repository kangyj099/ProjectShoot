using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState CurrentState { get; private set; }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;
        Debug.Log($"상태 머신 동작: {CurrentState} -> {newState}");

        ExitState(CurrentState);
        CurrentState = newState;
        EnterState(CurrentState);
    }

    private void ExitState(GameState state)
    {
        // 기존 상태 정리 (사운드매니저나 UI매니저 재정비 등)
    }

    private void EnterState(GameState state)
    {
        // 새 상태에 들어왔을 때 해 줄 게 있는지
        switch (state)
        {
            case GameState.MainMenu:
                break;

            case GameState.Loading:
                break;

            case GameState.Playing:
                break;

            default:
                break;
        }
    }
}
