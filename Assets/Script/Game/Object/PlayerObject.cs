using UnityEngine;

public class PlayerObject : ActorObject
{
    public override void InitData(ObjectData instance)
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
