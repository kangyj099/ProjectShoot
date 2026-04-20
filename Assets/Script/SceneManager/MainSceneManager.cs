using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : SingletonMono<MainSceneManager>
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    void Awake()
    {
        IsButtonNull();

        // 이벤트 연결
        startButton.onClick.AddListener(HandleStartButtonClicked);
        exitButton.onClick.AddListener(HandleExitButtonClicked);
    }

    void HandleStartButtonClicked()
    {
        GameRoot.Instance.SceneLoadManager.LoadScene(SceneType.Game, GameState.Playing);
    }

    void HandleExitButtonClicked()
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

    protected override void OnDestroyed()
    {
        // 씬이 파괴될 때 리스너를 제거
        startButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

    void IsButtonNull()
    {
        if (startButton == null) Debug.LogError("인스펙터에 startButton를 등록해주세요!");
        if (exitButton == null) Debug.LogError("인스펙터에 exitButton 등록해주세요!");
    }
}