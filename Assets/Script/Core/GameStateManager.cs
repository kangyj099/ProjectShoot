using UnityEngine;

public class GameStateManager
{
    public GameState CurrentState { get; private set; }

    public void Init()
    {
        CurrentState = GameState.MainMenu; // 첫 화면은 메인임.
        EnterState(CurrentState);
    }

    public void Release()
    {
        // 현재 특별히 처리할 내용은 없으나 형식 통일을 위한 함수
    }

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
                // 임시코드
                GameRoot.Instance.SoundManager.PlayBGM("TestBgm");
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
