using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ProjectileObjectData : BaseObject, IPoolable
{
    public SpriteRenderer SpriteRenderer { get; set; }

    public override ObjectType GetObjectType() => ObjectType.Projectile;
    public IPool Pool { get; set; }

    public float speed;
    public float damage;
    public LayerMask targetLayer;
    public ISkillBehavior behavior; // 탄환 추가 행동

    public float lifetime = 5f; //탄환 유지 시간
    private float timer;


    public void OnGet() => timer = 0f;

    public void OnRelease()
    {
        behavior = null; // 추가 행동 초기화
    }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // 행동 지침이 있으면 그에 따르고, 없으면 직진
        if (behavior != null)
        {
            behavior.Tick(this);
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        }

        // 시간 끝나면 풀에 돌려놓기
        timer += Time.fixedDeltaTime;
        if (timer >= lifetime)
        {
            Release();
        }
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
}