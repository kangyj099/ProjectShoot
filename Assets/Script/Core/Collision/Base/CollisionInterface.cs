using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICollsionPaticipant
{
    // 내 충돌이 어떤 충돌인지
    CollisionType CollType { get; }

}

public interface ICollisionSender : ICollsionPaticipant
{
    // 충돌 이벤트 발신할 때 동작
    ICollisionContext MakeCollisionContext(Collision2D collision);
}


public interface ICollisionReceiver : ICollsionPaticipant
{
    // 충돌 이벤트 수신할 때 동작
    bool ProccessCollisionContext(ICollisionContext collisionContext);
}

public interface ICollisionContext
{
    CollisionType CollType { get; }
}
