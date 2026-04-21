using UnityEngine;

[DisallowMultipleComponent]
public abstract class BaseObject : MonoBehaviour
{
    public abstract ObjectType GetObjectType();
    public virtual bool IsActor => false;   // ActorObject 자식 클래스에서만 true로 오버라이드
}
