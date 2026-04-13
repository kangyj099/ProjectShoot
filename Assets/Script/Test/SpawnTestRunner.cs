using UnityEngine;
using System.Collections.Generic;

// 오브젝트 스폰, 풀링 테스트 전용 스크립트
public class SpawnTestRunner : MonoBehaviour
{
    public PoolManager poolManager;
    public TestPoolable testPrefab;

    public List<IPoolable> activeObjects = new();

    public void GetObject()
    {
        var obj =  poolManager.Get(testPrefab);
        activeObjects.Add(obj);
    }

    public void ReleaseObject()
    {
        if (activeObjects.Count > 0)
        {
            var obj = activeObjects[0];
            obj.Release();
            activeObjects.RemoveAt(0);
        }
    }
}
