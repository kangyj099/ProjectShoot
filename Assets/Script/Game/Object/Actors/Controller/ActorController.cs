using UnityEngine;

public class ActorController : MonoBehaviour
{
    protected Movement movement;

    ActorStateMachine stateManager;

    void Awake()
    {
        movement = gameObject.GetComponent<Movement>();

        // 상태머신 초기화
        stateManager = CreateStateManager();
        stateManager.Init();

        OnAwake();
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    protected virtual void OnAwake() { }

    // 상태머신 생성 함수
    // 자식이 별도의 상태머신매니저를 사용할 경우 이 함수 override하기
    protected virtual ActorStateMachine CreateStateManager()
    {
        return new ActorStateMachine(this);
    }

}
