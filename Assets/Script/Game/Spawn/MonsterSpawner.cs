using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 몬스터를 스폰할 스폰 위치 그룹을 선택함
    // 스폰 위치 그룹 안에 있는 Transform중 랜덤한 하나 선택해 스폰 몬스터의 초기 위치, 각도를 세팅함
    public SpawnTransformGroups SpawnTransformGroups { get; private set; }
    public MonsterObjectData testMonsterPrefabs;

    private ObjectSpawner objectSpawner;

    // TEST: 테스트용 몬스터 스폰 로직
    private float testSpawnInterval = 2f; // 몬스터 스폰 간격 (초)
    private float testTimeSinceLastSpawn = 0f; // 마지막 스폰 이후 경과 시간

    public void SetTransformGroups(SpawnTransformGroups spawnTransformGroups)
    {
        SpawnTransformGroups = spawnTransformGroups;
        Debug.Log($"몬스터 스포너 시작: {SpawnTransformGroups.GroupCount}개의 스폰 위치 그룹이 설정됨");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() { }
    void Start()
    {
        objectSpawner = GameSceneManager.Instance.Spawner;
    }

    // Update is called once per frame
    void Update()
    {
        // TEST: 테스트용 몬스터 스폰 로직
        testTimeSinceLastSpawn += Time.deltaTime;
        if (testTimeSinceLastSpawn >= testSpawnInterval)
        {
            SpawnMonster(testMonsterPrefabs, Random.Range(0, SpawnTransformGroups.GroupCount)).Forget();
            testTimeSinceLastSpawn = 0f;
        }
    }

    public async UniTask<MonsterObject> SpawnMonster(MonsterObjectData monsterPrefab, int posGroupIndex)
    {
        Transform spawnTransform = SpawnTransformGroups.GetRandomTransform(posGroupIndex);
        if (spawnTransform == null)
        {
            Debug.LogError($"몬스터 {monsterPrefab.name} 스폰 실패: 몬스터 스폰 위치 그룹 {posGroupIndex}에 유효한 스폰 위치가 없음");

            return null;
        }

        var monster = await objectSpawner.SpawnObject(monsterPrefab, spawnTransform.position, spawnTransform.rotation, null) as MonsterObject;
        Debug.Log($"몬스터 {monsterPrefab.name} 스폰 성공: 위치 그룹 {posGroupIndex}");
        return monster;
    }
}
