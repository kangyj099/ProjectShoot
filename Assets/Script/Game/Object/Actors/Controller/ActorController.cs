using Cysharp.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent]
public class ActorController : MonoBehaviour
{
    protected Movement movement;
    ActorStateMachine stateManager;

    [SerializeField] protected ActorObject actorObject;

    void Awake()
    {
        movement = gameObject.GetComponent<Movement>();

        // 상태머신 초기화
        stateManager = CreateStateManager();
        stateManager.Init();

        if (actorObject == null)
        {
            Debug.LogError($"{gameObject.name} : ActorController의 actorObject가 할당되지 않았습니다.");
        }
        actorObject.SubscribeDeath(HandleDead);

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


    // HP 0이 되었을 때 호출할 상태머신 변경 함수
    private void HandleDead()
    {
        stateManager.ChangeState(ActorState.Die);
    }

    public async UniTaskVoid StartDeathSequence()
    {
        // 죽는 애니메이션 재생, 콜라이더 비활성화, 오브젝트 풀로 반환 등 죽는 시퀀스 처리
        Debug.Log($"{gameObject.name}이(가) 사망했습니다.");

        // TODO. 애니메이션 재생
        await UniTask.Delay(1000); // 예시로 1초 대기 (애니메이션 재생 시간)

        // BaseObject에서 반환하도록 함
        actorObject.Release();
    }
}
