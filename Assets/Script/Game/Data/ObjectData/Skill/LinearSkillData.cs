using UnityEngine;

[CreateAssetMenu(fileName = "NewNormalBullet", menuName = "Scriptable Object/Object Data/NormalBulletData")]

public class LinearSkillData : SkillData
{
    public override void Tick(ProjectileObject projectile, float moveDist)
    {
        projectile.transform.Translate(Vector3.up * moveDist);
    }
}