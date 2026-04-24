using UnityEngine;

public interface ISkillBehavior
{
    void Tick(ProjectileObjectData proj); // 매 프레임 실행할 로직
    void OnHit(ProjectileObjectData proj, Collider2D other); // 충돌 시 로직
}

public abstract class SkillData : ObjectData
{
    [Header("management")]
    public string skillName;

    [Header("Common Setting")]
    public Sprite SkillImg;
    public float speed; // 탄환 속도
    public float fireRate; // 탄 발사 속도
    public int shotCount; // 탄 몇 줄?
    public float shotInterval; // 탄 간격?
    public float spreadAngle; //퍼지는 각도
    public float damage;

    //최소최대치 보정
    public void ClampValue()
    {
        speed = System.Math.Max(0, speed);
        speed = System.Math.Min(speed, SkillConfig.MAX_SKILL_SPEED);

        fireRate = System.Math.Max(0, fireRate);
        fireRate = System.Math.Min(fireRate, SkillConfig.MAX_SKILL_FIRERATE);

        shotCount = System.Math.Max(0, shotCount);
        shotCount = System.Math.Min(shotCount, SkillConfig.MAX_SKILL_SHOTCOUNT);

        shotInterval = System.Math.Max(0, shotInterval);
        shotInterval = System.Math.Min(shotInterval, SkillConfig.MAX_SKILL_SHOTINTERVAL);

        damage = System.Math.Min(damage, float.MaxValue);
    }
}
