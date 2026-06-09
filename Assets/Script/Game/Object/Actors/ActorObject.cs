using UnityEngine;

public abstract class ActorObject : BaseObject
{
    public override bool IsActor => true;    // ActorObject 자식 클래스에서 true로 오버라이드

    protected Health HP;

    protected override void OnAwake()
    {
        base.OnAwake();

        TryGetComponent<Health>(out HP);
    }

    protected override void InitCollisionEntity()
    {
        if (HP)
        {
            CollisionEntity.AddCollisionReceiver(HP);
        }
    }

    public override void Release()
    {
        Destroy(gameObject);
    }
}
