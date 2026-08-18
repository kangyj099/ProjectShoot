using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 몬스터를 스폰할 스폰 위치 그룹을 선택함
    // 스폰 위치 그룹 안에 있는 Transform중 랜덤한 하나 선택해 스폰 몬스터의 초기 위치, 각도를 세팅함
    public SpawnTransformGroups SpawnTransformGroups { get; private set; }
    private ObjectSpawner objectSpawner;
    public StageMonsterWavesSO MonsterWavesData { get; private set; }
    public BehaviourPatternSO testBehaviourPattern;

    //TEST
    float tick = 3.1f;
    //TEST

    public void SetStage(Stage stage)
    {
        SpawnTransformGroups = stage.MonsterSpawnTransformGroups;
        MonsterWavesData = stage.MonsterWavesData;
        Debug.Log($"몬스터 스포너 시작: {SpawnTransformGroups.GroupCount}개의 스폰 위치 그룹이 설정됨");
    }

    void Start()
    {
        objectSpawner = GameSceneManager.Instance.Spawner;
    }

    void Update()
    {
        tick += Time.deltaTime;
        if (tick < 3.0f)
        {
            return;
        }

        tick = 0.0f;

        int posGroupIndex = Random.Range(0, SpawnTransformGroups.GroupCount);
        Transform spawnTransform = SpawnTransformGroups.GetRandomTransform(posGroupIndex);
        if (spawnTransform == null)
        {
            Debug.LogError($"몬스터 {MonsterWaveData.name} 스폰 실패: 몬스터 스폰 위치 그룹 {posGroupIndex}에 유효한 스폰 위치가 없음");
        }

        Vector3 offset = Vector3.zero;
        foreach (var monsterData in MonsterWaveData.monsterList)
        {
            Vector3 position = spawnTransform.position + offset;
            Debug.Log($"몬스터 {monsterData.name} 스폰 위치: {spawnTransform.position}");
            SpawnMonster(monsterData, position, spawnTransform.rotation, testBehaviourPattern).Forget();

            // 중심 몬스터 주변에 일정 간격만큼 떨어진 곳에 소환
            if (offset.x < 0)
            {
                offset *= -1;
            }
            else
            {
                offset = -offset + Vector3.left * 1.5f;
            }
        }
    }

    public async UniTask<MonsterObject> SpawnMonster(MonsterObjectData monsterPrefab, Vector3 position, Quaternion? rotation = null, BehaviourPatternSO behaviourPattern = null)
    {
        if (rotation == null)
        {
            rotation = Quaternion.identity;
        }


        var monster = await objectSpawner.SpawnPoolObject(monsterPrefab, position, rotation.Value) as MonsterObject;
        if (behaviourPattern != null)
        {
            monster.Controller.SetBehaviourPattern(behaviourPattern);
        }
        return monster;
    }

    public async UniTask<MonsterObject> SpawnNonPoolMonster(MonsterObjectData monsterPrefab, Vector3 position, Quaternion? rotation = null, BehaviourPatternSO behaviourPattern = null)
    {
        if (rotation == null)
        {
            rotation = Quaternion.identity;
        }

        var monster = await objectSpawner.SpawnObject(monsterPrefab, position, rotation.Value, null) as MonsterObject;
        if (behaviourPattern != null)
        {
            monster.Controller.SetBehaviourPattern(behaviourPattern);
        }
        return monster;
    }
}
