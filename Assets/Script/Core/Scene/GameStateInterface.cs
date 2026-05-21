using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 실행시
public class InitState : IState
{
    public void OnEnter() { }

    public void OnUpdate() { }

    public void OnExit() { }
}

// 메인 화면 상태
public class MainState : IState
{
    public void OnEnter() 
    {
        _ = GameRoot.Instance.UIManager.ShowSceneUI<UI_MainScene>();
        GameRoot.Instance.SoundManager.PlayBGM("TestBgm"); // 추후 수정
    }

    public void OnUpdate() { }

    public void OnExit() 
    {
        GameRoot.Instance.UIManager.ClearSceneUI();
    }
}

// 메인 화면 상태
public class LoadingState : IState
{
    public void OnEnter()
    {
        // 로딩 상태로 변경하면 자동으로 로딩씬으로 이동
        SceneManager.LoadScene((int)SceneType.Loading);
    }

    public void OnUpdate() { }

    public void OnExit() { }
}

// 메인 게임 플레이 상태
public class PlayingState : IState
{
    public void OnEnter()
    {
        Time.timeScale = 1.0f;
    }

    public void OnUpdate() { }

    public void OnExit() { }
}

// 일시정지/메뉴 상태
public class PauseState : IState
{
    public void OnEnter()
    {
        Time.timeScale = 0f;
    }

    public void OnUpdate() { }

    public void OnExit()
    {

    }
}