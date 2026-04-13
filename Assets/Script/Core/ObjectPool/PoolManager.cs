using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Dictionary<IPoolable, IPool> pools = new();

    public T Get<T>(T prefab) where T : Component, IPoolable
    {
        // 풀 없으면 새로운 풀 생성
        if (!pools.TryGetValue(prefab, out var pool))
        {
            // 풀 객체 모아둘 그룹 만들기
            GameObject poolParent = new GameObject(prefab.name + " Pool");
            poolParent.transform.SetParent(transform);

            // 풀 만들기
            var newPool = new Pool<T>(prefab, poolParent.transform);
            pools[prefab] = newPool;
            pool = newPool;
        }

        return ((Pool<T>)pool).Get();
    }

    public void Release<T>(T item) where T : Component, IPoolable
    {
        if (item.Pool != null)
        {
            item.Pool.Release(item);
        }
    }

    private void OnDestroy()
    {
        foreach (var pool in pools.Values)
        {
            pool.Clear();
        }
        pools.Clear();
    }

    public void Dispose<T>(T prefab) where T : Component, IPoolable
    {
        if (pools.TryGetValue(prefab, out var pool))
        {
            pool.Clear();
            pools.Remove(prefab);
        }
    }
}