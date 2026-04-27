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

    [Header("Collision Settings")]
    [Tooltip("Documents/기능설명/터널링문제.ppt 참고.\n체크하면 서클캐스트, 체크하지 않으면 레이캐스트")]
    public bool useCircleCast = false;
    [Tooltip("서클캐스트 선택 시에만 이용: 반지름")]
    public float colliderRadius = 0.1f;
    [Tooltip("레이캐스트/서클캐스트 시각화.")]
    public bool drawDebugGizmo = false;

    //최소최대치 보정
    public void ClampValue()
    {
        speed = Mathf.Clamp(speed, 0, SkillConfig.MAX_SKILL_SPEED);
        fireRate = Mathf.Clamp(fireRate, 0, SkillConfig.MAX_SKILL_FIRERATE);
        shotCount = Mathf.Clamp(shotCount, 0, SkillConfig.MAX_SKILL_SHOTCOUNT);
        shotInterval = Mathf.Clamp(shotInterval, 0, SkillConfig.MAX_SKILL_SHOTINTERVAL);

        damage = System.Math.Min(damage, float.MaxValue);

        colliderRadius = Mathf.Clamp(colliderRadius, 0.1f, float.MaxValue);
    }
}
