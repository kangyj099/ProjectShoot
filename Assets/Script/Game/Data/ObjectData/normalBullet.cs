using UnityEngine;

[CreateAssetMenu(fileName = "NewNormalBullet", menuName = "Scriptable Object/Object Data/NormalBulletData")]

public class LinearSkillData : SkillData
{
    public override void Initialize(BaseObject instance)
    {
        ClampValue();

        if (instance is ProjectileObject bullet)
        {
            if (SkillImg != null) bullet.SpriteRenderer.sprite = SkillImg;
            bullet.SpriteRenderer.sprite = SkillImg;
            bullet.speed = speed;
            bullet.damage = damage;
            bullet.behavior = null;

            bullet.useCircleCast = useCircleCast;
            bullet.colliderRadius = colliderRadius;
            bullet.drawDebugGizmo = drawDebugGizmo;
        }
    }
}