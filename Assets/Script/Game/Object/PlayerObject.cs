using UnityEngine;

public class PlayerObject : ActorObject
{
    public override void SetData(ObjectData data)
    { }

    public override ObjectType GetObjectType()
    {
        return ObjectType.Player;
    }

    protected override void InitCollisionEntity()
    {
        base.InitCollisionEntity();
    }
}
