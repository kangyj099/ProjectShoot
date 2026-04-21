using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Dictionary<GameObject, IPool> pools = new();

    public T Get<T>(ObjectData data) where T : IPoolable
    {
        var poolable = data.Prefab as IPoolable;
        if (poolable == null)
        {
            if (TryGetComponent(out IPoolable poolableComponent))
            {
                poolable = poolableComponent;
                Debug.LogWarning($"풀에서 생성할 오브젝트의 컴포넌트는 BaseObject와 IPoolable 컴포넌트를 같이 상속해야 함.\n{data.Prefab.gameObject.name}의 {data.Prefab.name}은 IPoolable 컴포넌트가 아님");
            }
            else
            {
                Debug.LogError($"풀에서 생성할 오브젝트는 반드시 IPoolable 컴포넌트를 가지고있어야 함.\n{data.Prefab.gameObject.name}의 {data.Prefab.name}은 IPoolable 컴포넌트가 아님");
                return default;
            }
        }

        // 풀 없으면 새로운 풀 생성
        if (!pools.TryGetValue(data.Prefab.gameObject, out var pool))
        {
            // 풀 객체 모아둘 그룹 만들기
            GameObject poolParent = new(data.Prefab.gameObject.name + " Pool");
            poolParent.transform.SetParent(transform);

            var componentType = poolable.GetType();

            // 풀 타입 만들기 (Pool<componentType>)이라는 타입을 생성
            var poolType = typeof(Pool<>).MakeGenericType(componentType);
            // 동적으로 생성한 타입으로 객체 생성
            pool = (IPool)Activator.CreateInstance(poolType, data.Prefab, poolParent.transform, 64, 1024);
            pools[data.Prefab.gameObject] = pool;
        }

        return (T)pool.Get();
    }

    public bool Release<T>(T item) where T : IPoolable
    {
        if (item.Pool == null)
        {
            Debug.LogError($"반환할 풀이 없음.");
            return false;
        }

        item.Pool.Release(item);
        return true;
    }

    public bool Release(GameObject obj)
    {
        var poolable = obj.GetComponent<IPoolable>();
        if (poolable == null)
        {
            Debug.LogError($"풀에 반환할 오브젝트는 반드시 IPoolable 컴포넌트를 가지고있어야 함.\n{obj.name}은 IPoolable 컴포넌트가 없음");
            return false;
        }

        poolable.Pool.Release(poolable);
        return true;
    }

    private void OnDestroy()
    {
        foreach (var pool in pools.Values)
        {
            pool.Clear();
        }
        pools.Clear();
    }

    public void Dispose(ObjectData data)
    {
        if (pools.TryGetValue(data.Prefab.gameObject, out var pool))
        {
            pool.Clear();
            pools.Remove(data.Prefab.gameObject);
        }
    }
}