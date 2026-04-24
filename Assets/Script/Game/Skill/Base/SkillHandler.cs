using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    public Transform muzzle;
    public LayerMask targetLayer;

    public SkillData defaultSkill;   // 기본 탄환
    public SkillData currentSkill;   // 현재 탄환

    [Header("Settings")]
    public bool isPlayer = false;

    private float nextFireTime = 0f;

    void Start()
    {
        if (currentSkill == null)
        {
            currentSkill = defaultSkill;
        }
    }

    void Update()
    {
        if (currentSkill != null && Time.time >= nextFireTime)
        {
            Fire(currentSkill);
            nextFireTime = Time.time + currentSkill.fireRate;
        }
    }

    public void ChangeSkill(SkillData newSkill)
    {
        currentSkill = newSkill;
    }

    public void Fire(SkillData data)
    {
        if (data == null || data.Prefab == null) return;
        
        float startOffset = -(data.shotInterval * (data.shotCount - 1)) / 2f;
        float startAngle = -(data.spreadAngle * (data.shotCount - 1)) / 2f;
        
        for (int i = 0; i < data.shotCount; i++)
        {
            // 오프셋
            float currentOffset = startOffset + (data.shotInterval * i);
            Vector3 spawnPosition = muzzle.position + (muzzle.right * currentOffset);

            // 각도
            float currentAngle = startAngle + (data.spreadAngle * i);
            Quaternion rotation = muzzle.rotation * Quaternion.Euler(0, 0, currentAngle);

            // PoolManager에서 객체 가져오기
            var obj = GameSceneManager.Instance.poolManager.Get<ProjectileObjectData>(data);

            if (obj != null)
            {
                obj.transform.position = spawnPosition;
                obj.transform.rotation = rotation;

                // 스탯 주입 (데이터 자체가 behavior를 가질 경우 자동으로 주입됨)
                data.Initialize(obj);

                // 공통 레이어 주입
                if (obj is ProjectileObjectData p)
                {
                    p.targetLayer = targetLayer;
                }
            }
        }
    }
}