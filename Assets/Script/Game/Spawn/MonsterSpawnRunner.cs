using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawnRunner : MonoBehaviour
{
    // 몬스터를 스폰할 스폰 위치 그룹을 선택함
    // 스폰 위치 그룹 안에 있는 Transform중 랜덤한 하나 선택해 스폰 몬스터의 초기 위치, 각도를 세팅함
    public SpawnTransformGroups SpawnTransformGroups { get; private set; }
    private ObjectSpawner objectSpawner;
    public StageMonsterWavesSO MonsterWavesData { get; private set; }
    List<MonsterWave> MonsterWaves => MonsterWavesData.monsterWaves;

    // stage 세팅하고 나서 지난 시간(tick, 초단위) MonsterWave.triggerTime 비교값
    float stageRunTick = 0.0f;
    // 다음 소환할 Wave 번호
    int nextWaveIndex = 0;


    public void SetStage(Stage stage)
    {
        SpawnTransformGroups = stage.MonsterSpawnTransformGroups;
        MonsterWavesData = stage.MonsterWavesData;
        stageRunTick = 0.0f;
        nextWaveIndex = 0;
        Debug.Log($"몬스터 스포너 시작: {SpawnTransformGroups.GroupCount}개의 스폰 위치 그룹이 설정됨");
    }

    void Start()
    {
        objectSpawner = GameSceneManager.Instance.Spawner;
    }

    void Update()
    {
        stageRunTick += Time.deltaTime;

        // 웨이브 소환
        if (nextWaveIndex < MonsterWaves.Count)
        {
            var nextWave = MonsterWaves[nextWaveIndex];
            if (stageRunTick >= nextWave.triggerTime)
            {
                Debug.Log($"몬스터 웨이브 소환: {nextWaveIndex}번째 웨이브, triggerTime={nextWave.triggerTime}");
                SpawnMonsterWave(nextWave.monsterGroup, nextWave.pattern).Forget();
                nextWaveIndex++;
            }
        }
    }

    public async UniTaskVoid SpawnMonsterWave(MonsterGroupSO monsterGroup, BehaviourPatternSO behaviourPattern)
    {
        int posGroupIndex = Random.Range(0, SpawnTransformGroups.GroupCount);
        Transform spawnTransform = SpawnTransformGroups.GetRandomTransform(posGroupIndex);
        if (spawnTransform == null)
        {
            Debug.LogError($"몬스터 {monsterGroup.name} 스폰 실패: 몬스터 스폰 위치 그룹 {posGroupIndex}에 유효한 스폰 위치가 없음");
            return;
        }

        Vector3 offset = Vector3.zero;
        // 그룹 내 몬스터 모두 소환 후 동시에 active
        List<MonsterObject> monsters = new List<MonsterObject>(monsterGroup.monsterList.Count);
        foreach (var monsterData in monsterGroup.monsterList)
        {
            Vector3 position = spawnTransform.position + offset;

            //몹 비활성화로 담아두기
            var monster = await SpawnMonster(monsterData, position, spawnTransform.rotation, behaviourPattern);
            monsters.Add(monster);
            monster.gameObject.SetActive(false);

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

        // 모든 몬스터 소환 후 동시에 활성화
        foreach (var monster in monsters)
        {
            monster.gameObject.SetActive(true);
        }
    }

    public async UniTask<MonsterObject> SpawnMonster(MonsterObjectData monsterPrefab, Vector3 position, Quaternion? rotation = null, BehaviourPatternSO behaviourPattern = null)
    {
        var rot = rotation ?? Quaternion.identity;

        var monster = await objectSpawner.SpawnPoolObject(monsterPrefab, position, rot) as MonsterObject;
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
