using UnityEngine;
using DG.Tweening;


//전체 
[DisallowMultipleComponent]
public sealed class GameRoot : SingletonMonoDontDestroy<GameRoot>
{
    public GameStateManager GameStateManager { get; private set; }

    public SceneLoadManager SceneLoadManager { get; private set; }

    public InputActionManager InputActionManager { get; private set; }

    public UIManager UIManager { get; private set; }

    public SoundManager SoundManager { get; private set; }

    protected override void OnAwake()
    {
        InitManagers(); //매니저 초기화
        InitDOTween();
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
        UIManager?.Release();
        SoundManager?.Release();
    }

    private void InitManagers()
    {
        // 컴포넌트 캐싱
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

        UIManager = new UIManager();
        UIManager.Init();
    }

    private void InitDOTween()
    {
        DOTween.Init(recycleAllByDefault: true, useSafeMode: true, LogBehaviour.ErrorsOnly);

        // Warmup 코드: 트윈용 메모리 풀 용량 확보
        DOTween.SetTweensCapacity(tweenersCapacity: 200, sequencesCapacity: 50);
    }
}
