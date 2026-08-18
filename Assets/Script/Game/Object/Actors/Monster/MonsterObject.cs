using UnityEngine;

// 몬스터 기본 스크립트,

public class MonsterObject : ActorObject
{
    public override ObjectType GetObjectType() => ObjectType.Monster;
    protected MonsterController controller;
    public MonsterController Controller => controller;

    SpriteRenderer spriteRenderer;

    public override void SetData(ObjectData data)
    {
        MonsterObjectData monsterData = data as MonsterObjectData;
        if (monsterData == null)
        {
            Debug.LogError($"{GetType().Name}에는 MonsterObjectData가 필요합니다. 엉뚱한 데이터로 초기화를 시도하고있습니다..");
        }

        if (spriteRenderer)
        {
            spriteRenderer.sprite = monsterData.Sprite;
        }

        if (HP)
        {
            HP.SetData(this, monsterData.MaxHp, monsterData.MaxHp, monsterData.MaxHp);
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        TryGetComponent<SpriteRenderer>(out spriteRenderer);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Release()
    {
        Destroy(gameObject);
    }

    public void BindController(MonsterController controller)
    {
        this.controller = controller;
    }


}
