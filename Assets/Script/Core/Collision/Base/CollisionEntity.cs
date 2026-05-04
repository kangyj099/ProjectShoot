using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEntity
{
    readonly private BaseObject owner;

    readonly private List<ICollisionSender>[] collisionSenders = new List<ICollisionSender>[(int)(CollisionType.Count)];
    readonly private List<ICollisionReceiver>[] collisionReceivers = new List<ICollisionReceiver>[(int)(CollisionType.Count)];

    public CollisionEntity(BaseObject baseObject)
    {
        this.owner = baseObject;
        for (int i = 0; i < (int)CollisionType.Count; i++)
        {
            collisionSenders[i] = new List<ICollisionSender>();
            collisionReceivers[i] = new List<ICollisionReceiver>();
        }
    }

    public bool AddCollisionSender(ICollisionSender collisionSender)
    {

        if (collisionSenders[(int)collisionSender.CollType].Contains(collisionSender))
        {
            return false;
        }

        collisionSenders[(int)collisionSender.CollType].Add(collisionSender);
        Debug.Log($"{owner.gameObject.name}에 충돌센더 {collisionSender.CollType} 부착");

        return true;
    }

    public bool AddCollisionReceiver(ICollisionReceiver collisionReceiver)
    {
        if (collisionReceivers[(int)collisionReceiver.CollType].Contains(collisionReceiver))
        {
            return false;
        }
        collisionReceivers[(int)collisionReceiver.CollType].Add(collisionReceiver);

        Debug.Log($"{owner.gameObject.name}에 충돌리시버 {collisionReceiver.CollType} 부착");
        return true;
    }

    public bool SendCollisionContext(in HitInfo hitInfo)
    {
        if (hitInfo.Target == null)
            throw new ArgumentNullException(nameof(hitInfo.Target));

        foreach (var senderList in collisionSenders)
        {
            foreach (var sender in senderList)
            {
                var context = sender.MakeCollisionContext(in hitInfo);
                hitInfo.Target.CollisionEntity.ReceiveCollisionContext(context);
                Debug.Log($"{hitInfo.Target.gameObject.name}에게 {context.CollType}충돌 전달");
            }
        }

        return true;
    }

    public bool ReceiveCollisionContext(ICollisionContext context)
    {
        if (context == null)
        {
            return false;
        }

        foreach (var receiver in collisionReceivers[(int)context.CollType])
        {
            if (false == receiver.ProccessCollisionContext(context))
            {
                Debug.LogError($"충돌 이벤트 처리 실패 - Receiver: {receiver}, Context: {context}");
            }
            Debug.Log($"{owner.name} 충돌 이벤트 {context.CollType}수신 처리");
        }

        return true;
    }
}