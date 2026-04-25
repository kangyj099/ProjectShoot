using UnityEngine;

[CreateAssetMenu(fileName = "NewNormalBullet", menuName = "Scriptable Object/Object Data/NormalBulletData")]

public class LinearSkillData : SkillData
{
    public override void Initialize(BaseObject instance)
    {
        ClampValue();

        if (instance is ProjectileObject bullet)
        {
            bullet.SpriteRenderer.sprite = SkillImg;
            bullet.speed = speed;
            bullet.damage = damage;
            bullet.behavior = null;
        }
    }
}