using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;

    public async UniTask<IPoolable> SpawnPoolObject(ObjectData data, Vector3 position, Quaternion rotation)
    {
        // 객체에 필요한 리소스 로드
        { }

        // 데이터 기반 객체 생성, 초기화
        var poolable = poolManager.Get<IPoolable>(data);
        if (poolable == default)
        {
            return null;
        }
        
        var baseObj = poolable as BaseObject;
        if (baseObj == null)
        {
            return null;
        }
        data.Initialize(baseObj);

        var obj = baseObj.gameObject;
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return poolable;
    }
}
