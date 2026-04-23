using UnityEngine;

public interface ISkillBehavior
{
    void Tick(ProjectileObjectData proj); // 매 프레임 실행할 로직
    void OnHit(ProjectileObjectData proj, Collider2D other); // 충돌 시 로직
}

public abstract class SkillData : ObjectData
{
    [Header("management")]
    public int skillIdx;
    public string skillName;

    [Header("Common Setting")]
    public float speed; // 탄환 속도
    public float fireRate; // 탄 발사 속도
    public int damage;
    public int ShotCount; // 탄 몇 줄?
    public float spreadAngle; //퍼지는 각도

    public Sprite sprite;
}
