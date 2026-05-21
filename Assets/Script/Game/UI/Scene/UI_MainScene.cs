using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainScene : UI_Scene
{
    enum Buttons
    {
        StartButton,
        SettingsButton,
        ExitButton
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        // 클릭 이벤트 연결
        BindEvent(GetButton((int)Buttons.StartButton).gameObject, OnClickStartButton);
        BindEvent(GetButton((int)Buttons.SettingsButton).gameObject, OnClickSettingsButton);
        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnClickExitButton);
    }

    public override void Release()
    {
        base.Release();
    }

    private void OnClickStartButton(PointerEventData eventData)
    {
        GameRoot.Instance.SceneLoadManager.LoadScene(SceneType.Game, GameState.Playing);
    }

    private void OnClickSettingsButton(PointerEventData eventData)
    {
        _ = GameRoot.Instance.UIManager.ShowPopupUI<UI_SettingsPopup>();
    }

    private void OnClickExitButton(PointerEventData eventData)
    {
        // 종료 로직
#if UNITY_EDITOR
        // 에디터에서 실행 중일 때
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서 실행 중일 때
        Application.Quit();
#endif
    }
}