using UnityEngine;

public class PlayerObject : ActorObject
{
    public override ObjectType GetObjectType()
    {
        return ObjectType.Player;
    }
}
