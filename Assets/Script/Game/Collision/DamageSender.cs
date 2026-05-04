using UnityEngine;

[RequireComponent(typeof(BaseObject))]
public class DamageSender: MonoBehaviour, ICollisionSender
{
    BaseObject owner;
    public int Damage { get; private set; }
    public CollisionType CollType => CollisionType.Damage;

    public ICollisionContext MakeCollisionContext(in HitInfo hitInfo)
    {
        return new DamageCollCtx(Damage);
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }
}
