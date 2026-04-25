using UnityEngine;

[DisallowMultipleComponent]
public abstract class BaseObject : MonoBehaviour
{
    public abstract ObjectType GetObjectType();
    public CollisionEntity CollisionEntity { get; private set; }
    public virtual bool IsActor => false;   // ActorObject 자식 클래스에서만 true로 오버라이드

    private void Awake()
    {
        OnAwake();

        // 충돌처리 담당 Entity 생성
        CollisionEntity = new CollisionEntity(this);
        InitCollisionEntity();
    }

    protected virtual void OnAwake()
    {
        // 자식 클래스에서 초기화 작업이 필요하면 이 메서드를 오버라이드해서 사용
    }

    /// <summary>
    /// 오브젝트에 관련된 충돌 Entity 초기화
    ///* - 충돌 발신자와 수신자 등록
    ///* - 충돌 발신자와 수신자 등록은 CollisionEntity의 AddCollisionSender, AddCollisionReceiver 메서드를 사용해서 등록
    ///* - 예시)
    ///*   CollisionEntity.AddCollisionSender(new DamageCollisionSender(...));
    ///*   CollisionEntity.AddCollisionReceiver(new BuffCollisionReceiver(...));
    /// </summary>
    protected abstract void InitCollisionEntity();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<BaseObject>(out var target);

        CollisionEntity.SendCollisionContext(target, collision); // 충돌 컨텍스트 생성 후 전송
    }
}
