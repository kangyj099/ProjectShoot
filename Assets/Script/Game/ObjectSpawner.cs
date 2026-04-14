using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;

    public async UniTask<IPoolable> SpawnPoolObject(ObjectData data, Vector3 position, Quaternion rotation)
    {
        var poolable = poolManager.Get<IPoolable>(data);
        data.Initialize(poolable);

        var obj = (poolable as Component).gameObject;
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return poolable;
    }
}
