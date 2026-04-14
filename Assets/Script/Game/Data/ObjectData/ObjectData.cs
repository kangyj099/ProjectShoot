using UnityEngine;

public abstract class ObjectData : ScriptableObject
{
    [SerializeField] private Component prefab;   // 풀링할 프리팹 리소스 원본, IPoolable 컴포넌트 필수 포함

    private void OnValidate()
    {
        if (prefab != null && prefab.GetComponent<IPoolable>() == null)
        {
            Debug.LogError($"{name}: IPoolable 컴포넌트가 없음");
            prefab = null;
        }
        else
        {
            prefab = prefab.GetComponent<IPoolable>() as Component;
            if (prefab == null)
            {
                Debug.LogError($"{name}: IPoolable이 컴포넌트가 아님");
            }
        }
    }

    public Component Prefab
    {
        get => prefab;
        set
        {
            if (value != null && value.GetComponent<IPoolable>() == null)
            {
                Debug.LogError($"{name}: IPoolable 컴포넌트가 없음");
                prefab = null;
            }
            else
            {
                prefab = value;
            }
        }
    }

    public abstract void Initialize(IPoolable instance);
}
