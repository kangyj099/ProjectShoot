using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MonsterObjectPoolable : MonsterObject, IPoolable
{
    public override ObjectType GetObjectType() => ObjectType.None;

    public IPool Pool { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    // BaseObject 추상 메서드 구현
    public override void SetData(ObjectData data)
    {
        MonsterObjectData monsterData = data as MonsterObjectData;
        if (monsterData == null)
        {
            Debug.LogError("오브젝트 세팅을 위한 몬스터 데이터가 필요합니다.");
            return;
        }
    }

    public override void Release()
    {
        Controller.SetBehaviourPattern(null);
        this.Return();
    }

    // IPooledObject 인터페이스 구현
    public void OnGet()
    {
        gameObject.SetActive(true);
    }
    public void OnReturn()
    {
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
