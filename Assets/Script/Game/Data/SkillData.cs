using UnityEngine;

public interface ISkillBehavior
{
    void Tick(ProjectileObject proj); // 매 프레임 실행할 로직
    void OnHit(ProjectileObject proj, Collider2D other); // 충돌 시 로직
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
        speed = Mathf.Clamp(speed, 0, SkillConfig.MAX_SKILL_SPEED);
        fireRate = Mathf.Clamp(fireRate, 0, SkillConfig.MAX_SKILL_FIRERATE);
        shotCount = Mathf.Clamp(shotCount, 0, SkillConfig.MAX_SKILL_SHOTCOUNT);
        shotInterval = Mathf.Clamp(shotInterval, 0, SkillConfig.MAX_SKILL_SHOTINTERVAL);

        damage = System.Math.Min(damage, float.MaxValue);
    }
}
