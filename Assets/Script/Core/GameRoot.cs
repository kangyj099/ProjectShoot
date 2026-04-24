using UnityEngine;


//전체 
[DisallowMultipleComponent]
public sealed class GameRoot : SingletonMonoDontDestroy<GameRoot>
{
    public GameStateManager GameStateManager { get; private set; }

    public SceneLoadManager SceneLoadManager { get; private set; }

    public InputActionManager InputActionManager { get; private set; }

    public SoundManager SoundManager { get; private set; }

    protected override void OnAwake()
    {
        InitManagers(); //매니저 초기화
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    protected override void OnDestroyed()
    {
        GameStateManager?.Release();
        SceneLoadManager?.Release();
        InputActionManager?.Release();
        SoundManager?.Release();
    }

    private void InitManagers()
    {
        // 컴포넌트 캐싱 - 인스펙터에서 확인하는 게 좋은 내용은 이쪽으로
        SoundManager = GetComponentInChildren<SoundManager>();
        if (SoundManager == null) Debug.LogError("SoundManager를 찾을 수 없습니다!");
        else SoundManager.Init();

        // 스크립트 초기화
        GameStateManager = new GameStateManager();
        GameStateManager.Init();

        SceneLoadManager = new SceneLoadManager();
        SceneLoadManager.Init();

        InputActionManager = new InputActionManager();
        InputActionManager.Init();

    }
}
