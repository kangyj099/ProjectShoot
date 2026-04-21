using UnityEngine;
using UnityEngine.Pool;

// Todo
// 웜업 코드 이상함 수정 필요
// prefab null 안되게 강제
// 디버그모드일 때 collectionCheck true, 아닐 때 false로 설정

public interface IPoolable
{
    /// <summary>
    /// 자기 소속 풀 참조
    /// </summary>
    IPool Pool { get; set; }

    /// <summary>
    /// 풀에서 꺼내올 때 실행할 동작
    /// </summary>
    void OnGet();
    /// <summary>
    /// 풀에 돌려놓을 때 실행할 동작
    /// </summary>
    void OnRelease();
}

public interface IPool
{
    IPoolable Get();
    void Release(IPoolable poolable);
    void Warmup(int initialSize);
    void Clear();
}

public class Pool<T> : IPool where T : BaseObject, IPoolable
{
    private readonly ObjectPool<T> pool;
    private readonly T prefab;   // 풀링할 프리팹 리소스 원본
    private readonly Transform parent; // 풀링된 객체의 부모 Transform (옵션)

    public IPoolable Get() => pool.Get();

    public void Release(IPoolable item) => pool.Release((T)item);


    public Pool(T prefab, Transform parent = null, int initialSize = 64, int maxSize = 1024)
    {
        this.prefab = prefab;
        this.parent = parent;

        pool = new ObjectPool<T>(
            OnCreate,
            OnGet,
            OnRelease,
            (poolableComp) => GameObject.Destroy(poolableComp.gameObject),
            collectionCheck: true,
            defaultCapacity: initialSize,
            maxSize: maxSize
        );

        // 웜업
        Warmup(initialSize);

        Debug.Log($"{prefab.name}풀 생성 : 개체수{pool.CountAll}(active{pool.CountActive}/inactive{pool.CountInactive})");
    }

    private T OnCreate()
    {
        var item = GameObject.Instantiate(prefab);
        item.Pool = this;
        if (parent != null)
        {
            item.transform.SetParent(parent);
        }
        return item;
    }

    private void OnGet(T item)
    {
        item.gameObject.SetActive(true);
        item.OnGet();
    }

    private void OnRelease(T item)
    {
        item.OnRelease();
        item.gameObject.SetActive(false);
    }

    public void Warmup(int initialSize)
    {
        if (initialSize <= 0 || initialSize > pool.CountAll)
        {
            return;
        }

        T [] tempArray = new T[initialSize];
        for (int i = 0; i < initialSize; i++)
        {
            tempArray[i] = pool.Get();
        }
        foreach(var item in tempArray)
        {
            pool.Release(item);
        }
    }

    public void Clear()
    {
        pool.Clear();
    }
}
