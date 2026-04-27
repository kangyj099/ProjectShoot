using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ProjectileObject : BaseObject, IPoolable
{
    public SpriteRenderer SpriteRenderer { get; set; }

    public override ObjectType GetObjectType() => ObjectType.Projectile;
    public IPool Pool { get; set; }

    public float speed;
    public float damage;
    public LayerMask targetLayer;
    public ISkillBehavior behavior; // 탄환 추가 행동

    public bool useCircleCast = false;
    public float colliderRadius = 0.1f;

    public float lifetime = 5f; //탄환 유지 시간
    private float timer;


    public void OnGet() => timer = 0f;

    public void OnRelease()
    {
        behavior = null; // 추가 행동 초기화
    }

    protected override void OnAwake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void InitCollisionEntity()
    {
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

    private void OnBecameInvisible()
    {
        // 화면 밖에 나가면 풀로 돌려놓기
        // 추후 수정 가능
        Release();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            // 코드 수정 필요
            // 플레이어 & 적일때만 충돌하도록 (총알일 경우 제외)
            // 대미지 적용 식 연동되도록
            behavior?.OnHit(this, other);
            Release();
        }
    }

    public void Release()
    {
        if (Pool != null) Pool.Release(this);
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

            // 충돌 이후 작업

        }
        else
        {
            Move(moveDist);
        }
    }

    private void Move(float moveDist)
    {
        // 행동 지침이 있으면 그에 따르고, 없으면 직진
        if (behavior != null)
        {
            behavior.Tick(this);
        }
        else
        {
            transform.Translate(Vector3.up * moveDist);
        }
    }
}