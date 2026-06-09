using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class ProjectileObject : BaseObject, IPoolable
{
    public SpriteRenderer SpriteRenderer { get; set; }
    private DamageSender damageSender;

    public override ObjectType GetObjectType() => ObjectType.Projectile;
    public IPool Pool { get; set; }
    public override void Release()
    {
        transform.DOKill(); // 모든 연산 강제 종료
        behavior = null; // 추가 행동 초기화

        this.Return();
    }

    public float speed;
    public float damage;
    public LayerMask targetLayer;
    public ISkillBehavior behavior; // 탄환 추가 행동

    public bool useCircleCast = false;
    public float colliderRadius = 0.1f;
    public bool drawDebugGizmo = false;

    public float lifetime = 5f; //탄환 유지 시간
    private float timer;

    public override void SetData(ObjectData data)
    {
        if (data is not SkillData skillData)
        {
            Debug.LogError($"{name}에 올바르지 않은 ObjectData({data?.GetType()})가 주입되었습니다.");
            return;
        }

        skillData.ClampValue();

        if (skillData.SkillImg != null) SpriteRenderer.sprite = skillData.SkillImg;
        speed = skillData.speed;
        damage = skillData.damage;
        behavior = skillData;

        useCircleCast = skillData.useCircleCast;
        colliderRadius = skillData.colliderRadius;
        drawDebugGizmo = skillData.drawDebugGizmo;

        if (damageSender)
        {
            damageSender.SetDamage((int)damage);
        }
    }

    public void OnGet() => timer = 0f;

    public void OnReturn() { }

    protected override void OnAwake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        if (damageSender == null) TryGetComponent(out damageSender);
    }

    protected override void InitCollisionEntity()
    {
        if (damageSender)
        {
            CollisionEntity.AddCollisionSender(damageSender);
        }
    }

    void FixedUpdate()
    {
        CheckCollision();

        // 시간 끝나면 풀에 돌려놓기
        timer += Time.fixedDeltaTime;
        if (timer >= lifetime)
        {
            Release();
        }
    }

    private void CheckCollision()
    {
        float moveDist = speed * Time.fixedDeltaTime;
        Vector2 direction = transform.up;
        RaycastHit2D hit;

        if (useCircleCast)
        {
            hit = Physics2D.CircleCast(transform.position, colliderRadius, direction, moveDist, targetLayer);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, direction, moveDist, targetLayer);
        }

        if (hit.collider != null)
        {
            // 다음 프레임 총알 위치가 충돌체에게 닿아 보이는 것처럼 세팅 (관통하지 않도록)
            // 혹시 테스트 후 이상하게 보이면 수정해야 함
            transform.position = useCircleCast ? (Vector3)hit.centroid : (Vector3)hit.point;

            // 충돌했다고 전달
            if (hit.transform.gameObject.TryGetComponent<BaseObject>(out BaseObject target))
            {
                HitInfo hitInfo = new HitInfo(hit, this, target);
                CollisionEntity.SendCollisionContext(hitInfo);
            }

            // 총알 반환
            Release();
        }
        else
        {
            Move(moveDist);
        }
    }

    private void Move(float moveDist)
    {
        if (behavior == null)
        {
            Debug.LogError($"이게 어떻게 가능한 진 몰라도 {this.name} 탄환 behavior이 null입니다!");
            return;
        }

        behavior.Tick(this, moveDist);
    }

    private void OnDrawGizmos()
    {
        if (!drawDebugGizmo) return;

        Gizmos.color = Color.red;
        Vector3 direction = transform.up;
        float moveDist = speed * Time.fixedDeltaTime;
        Vector3 nextPos = transform.position + direction * moveDist;

        if (useCircleCast)
        {
            // 서클캐스트 시각화
            Gizmos.DrawWireSphere(transform.position, colliderRadius);
            Gizmos.DrawWireSphere(nextPos, colliderRadius);

            Vector3 rightOffset = transform.right * colliderRadius;
            Gizmos.DrawLine(transform.position + rightOffset, nextPos + rightOffset);
            Gizmos.DrawLine(transform.position - rightOffset, nextPos - rightOffset);
        }
        else
        {
            // 레이캐스트 시각화
            Gizmos.DrawLine(transform.position, nextPos);
        }
    }
}