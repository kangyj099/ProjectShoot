using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "NewHomingBullet", menuName = "Scriptable Object/Object Data/HomingBulletData")]

public class HomingSkillData : SkillData
{
    [Header("Homing Settings")]
    [Tooltip("유도탄의 회전 속도 (높을수록 급격하게 꺾임)")]
    public float rotationSpeed = 5f;

    [Tooltip("적을 탐색할 반지름 범위")]
    public float detectionRadius = 5f;

    [Tooltip("유도 꺾임 그래프")]
    public Ease rotationEase = Ease.OutQuad;

    public override void Tick(ProjectileObject projectile, float moveDist)
    {
        // 타겟 탐색
        // 매 프레임에 하는 이유: 각 개체가 각자의 타겟을 갖고 있어야 하기 때문
        // 스크립터블 오브젝트는 에셋으로 생성되기 때문에 모든 유도탄이 같은 타겟 갖게 될 수도 있어서...
        Transform target = FindNearestTarget(projectile.transform.position, projectile.targetLayer);

        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - (Vector2)projectile.transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

            // 바로 직전 프레임에 생성되어 돌고 있던 회전 트윈을 완전히 Kill하여 중첩을 방지
            projectile.transform.DOKill();

            projectile.transform.DORotateQuaternion(targetRotation, 1f / rotationSpeed)
                .SetUpdate(UpdateType.Fixed) // FixedUpdate 타이밍과 동기화
                .SetEase(rotationEase);
        }

        projectile.transform.Translate(Vector3.up * moveDist);
    }

    private Transform FindNearestTarget(Vector2 currentPos, LayerMask targetLayer)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(currentPos, detectionRadius, targetLayer);
        if (targets.Length == 0) return null;

        Transform nearestTarget = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < targets.Length; i++)
        {
            float distance = Vector2.Distance(currentPos, targets[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestTarget = targets[i].transform;
            }
        }
        return nearestTarget;
    }
}