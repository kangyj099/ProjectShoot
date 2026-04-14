using UnityEngine;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using System;

// 오브젝트 스폰, 풀링 테스트 전용 스크립트
public class SpawnTestRunner : MonoBehaviour
{
    public PoolManager poolManager;
    public ObjectSpawner spawner;

    public TestObjectData testObjectData;

    public List<TestPoolable> activeObjects = new();

    public async void GetObject()
    {
        var obj =  poolManager.Get<TestPoolable>(testObjectData);
        activeObjects.Add(obj);
    }

    public void ReleaseObject()
    {
        if (activeObjects.Count > 0)
        {
            var obj = activeObjects[0];
            poolManager.Release<TestPoolable>(obj);
            activeObjects.RemoveAt(0);
        }
    }

    public async void SpawnPoolObject(ObjectData data)
    {
        var type = data.Prefab.GetType();
        var obj = await spawner.SpawnPoolObject(data, Vector3.zero, Quaternion.identity);
    }
}
