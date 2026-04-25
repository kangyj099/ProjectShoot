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

        return true;
    }

    public bool AddCollisionReceiver(ICollisionReceiver collisionReceiver)
    {
        if (collisionReceivers[(int)collisionReceiver.CollType].Contains(collisionReceiver))
        {
            return false;
        }
        collisionReceivers[(int)collisionReceiver.CollType].Add(collisionReceiver);
        return true;
    }

    public bool SendCollisionContext(BaseObject target, Collision2D collision)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        if (collision == null)
            throw new ArgumentNullException(nameof(collision));

        foreach (var senderList in collisionSenders)
        {
            foreach (var sender in senderList)
            {
                var context = sender.MakeCollisionContext(collision);
                target.CollisionEntity.ReceiveCollisionContext(context);
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
        }

        return true;
    }
}