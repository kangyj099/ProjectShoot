using UnityEngine;

[CreateAssetMenu(fileName = "NewNormalBullet", menuName = "Scriptable Objects/ObjectData/NewNormalBullet")]

public class LinearSkillData : SkillData
{
    public override void Initialize(BaseObject instance)
    {
        ClampValue();

        if (instance is ProjectileObjectData bullet)
        {
            bullet.SpriteRenderer.sprite = SkillImg;
            bullet.speed = speed;
            bullet.damage = damage;
            bullet.behavior = null;
        }
    }
}