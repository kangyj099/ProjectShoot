using UnityEngine;


//전체 
[DisallowMultipleComponent]
public sealed class GameRoot : SingletonMono<GameRoot>
{
    public SceneLoadManager SceneLoadManager { get; private set; }
    public GameStateManager GameStateManager { get; private set; }

    protected override void OnAwake()
    {
        InitManagers(); //매니저 컴포넌트
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    protected override void OnDestroyed()
    {
    }

    private void InitManagers()
    {
        SceneLoadManager = GetComponentInChildren<SceneLoadManager>();
        GameStateManager = GetComponentInChildren<GameStateManager>();

        // 매니저가 없을 경우 에러 메시지 출력
        if (SceneLoadManager == null) Debug.LogError("SceneLoadManager를 찾을 수 없습니다!");
        if (GameStateManager == null) Debug.LogError("GameStateManager를 찾을 수 없습니다!");
    }
}
