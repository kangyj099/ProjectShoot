using UnityEngine;

public abstract class ObjectData : ScriptableObject
{
    [SerializeField] private BaseObject prefab;   // 프리팹 리소스 원본, BaseObject 컴포넌트 필수 포함
    private ObjectID objectID;

    public BaseObject Prefab => prefab;
    public string GetID => objectID.ID;

    private void OnValidate()
    {
        if (prefab != null && prefab.GetComponent<ObjectID>() == null)
        {
            Debug.LogError($"{name}: ObjectID 컴포넌트가 없음");
        }
        else
        {
            objectID = prefab.GetComponent<ObjectID>();
        }

        if (prefab != null && prefab.GetComponent<BaseObject>() == null)
        {
            Debug.LogError($"{name}: BaseObject 컴포넌트가 없음");
        }
    }

    public abstract void Initialize(BaseObject instance);
}
