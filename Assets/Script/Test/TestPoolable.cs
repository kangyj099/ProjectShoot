using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TestPoolable : BaseObject, IPoolable
{
    public override ObjectType GetObjectType() => ObjectType.None;

    public IPool Pool { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    // IPooledObject 인터페이스 구현
    public void OnGet()
    {
        Debug.Log("TestPoolable OnGet");
        gameObject.SetActive(true);
    }
    public void OnRelease()
    {
        Debug.Log("TestPoolable OnRelease");
        gameObject.SetActive(false);
    }

    // 유니티 생명주기 메서드
    protected override void OnAwake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void InitCollisionEntity()
    {
        // TestPoolable은 충돌 처리가 필요 없으므로 빈 구현
    }
}
