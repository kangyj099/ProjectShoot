using UnityEngine;

public class DamageCollCtx : ICollisionContext
{
    public CollisionType CollType => CollisionType.Damage;
    public int damage;
    public DamageCollCtx(int damage)
    {
        this.damage = damage;
    }    
}
