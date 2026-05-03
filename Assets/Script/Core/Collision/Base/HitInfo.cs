using UnityEngine;

public struct HitInfo
{
    public BaseObject Attacker { get; }
    public BaseObject Target { get; }

    public HitInfo(Collision2D collision, BaseObject attacker, BaseObject target)
    {
        Attacker = attacker;
        Target = target;
    }

    public HitInfo(RaycastHit2D rayHit, BaseObject attacker, BaseObject target)
    {
        Attacker = attacker;
        Target = target;
    }
}