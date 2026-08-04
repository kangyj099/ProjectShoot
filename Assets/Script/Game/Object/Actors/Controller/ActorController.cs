using Cysharp.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent]
public class ActorController : MonoBehaviour
{
    protected Movement movement;

    [SerializeField] protected ActorObject actorObject;

    void Awake()
    {
        movement = gameObject.GetComponent<Movement>();

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
        OnUpdate();
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFixedUpdate() { }


    // HP 0이 되었을 때 호출할 상태머신 변경 함수
    private void HandleDead()
    {
        StartDeathSequence().Forget();
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
