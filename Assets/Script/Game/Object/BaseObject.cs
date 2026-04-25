using UnityEngine;

[DisallowMultipleComponent]
public abstract class BaseObject : MonoBehaviour
{
    public abstract ObjectType GetObjectType();
    public virtual bool IsActor => false;   // ActorObject 자식 클래스에서만 true로 오버라이드

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        // 자식 클래스에서 초기화 작업이 필요하면 이 메서드를 오버라이드해서 사용
    }
}
