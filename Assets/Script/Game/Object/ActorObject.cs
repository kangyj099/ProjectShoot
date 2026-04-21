using UnityEngine;

public abstract class ActorObject : BaseObject
{
    public override bool IsActor => true;    // ActorObject 자식 클래스에서 true로 오버라이드
}
