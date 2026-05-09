using UnityEngine;
using UnityEngine.SceneManagement;

public interface IState
{
    void OnEnter();
    void OnExit();
}

// 메인 화면 상태
public class MainState : IState
{
    public void OnEnter() { }

    public void OnExit() { }
}

// 메인 화면 상태
public class LoadingState : IState
{
    public void OnEnter()
    {
        // 로딩 상태로 변경하면 자동으로 로딩씬으로 이동
        SceneManager.LoadScene((int)SceneType.Loading);
    }

    public void OnExit() { }
}

// 메인 게임 플레이 상태
public class PlayingState : IState
{
    public void OnEnter()
    {
        Time.timeScale = 1.0f;
    }

    public void OnExit() { }
}

// 일시정지/메뉴 상태
public class PauseState : IState
{
    public void OnEnter()
    {
        Time.timeScale = 0f;
    }

    public void OnExit()
    {

    }
}