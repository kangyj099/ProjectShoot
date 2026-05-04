using UnityEngine;

public class PlayerObject : ActorObject
{
    public override void SetData(ObjectData data)
    {
        PlayerObjectData playerData = data as PlayerObjectData;
        if (playerData == null)
        {
            Debug.LogError($"{GetType().Name}에는 PlayerObjectData가 필요합니다. 엉뚱한 데이터로 초기화를 시도하고있습니다..");
        }

        if (HP)
        {
            HP.SetData(this, playerData.MaxHp, playerData.MaxHp, playerData.MaxHpCap);
        }
    }

    public override ObjectType GetObjectType()
    {
        return ObjectType.Player;
    }

    protected override void InitCollisionEntity()
    {
        base.InitCollisionEntity();
    }
}
