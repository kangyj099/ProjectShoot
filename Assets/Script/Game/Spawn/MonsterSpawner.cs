using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 몬스터를 스폰할 스폰 위치 그룹을 선택함
    // 스폰 위치 그룹 안에 있는 Transform중 랜덤한 하나 선택해 스폰 몬스터의 초기 위치, 각도를 세팅함
    public SpawnTransformGroups SpawnTransformGroups { get; private set; }

    private ObjectSpawner objectSpawner;

    public void SetTransformGroups(SpawnTransformGroups spawnTransformGroups)
    {
        SpawnTransformGroups = spawnTransformGroups;
        Debug.Log($"몬스터 스포너 시작: {SpawnTransformGroups.GroupCount}개의 스폰 위치 그룹이 설정됨");
    }

    public void SetMonsterSpawnData()
    {

    }

    void Start()
    {
        objectSpawner = GameSceneManager.Instance.Spawner;
    }

    public async UniTask<MonsterObject> SpawnMonster(MonsterObjectData monsterPrefab, int posGroupIndex, BehaviourPatternSO behaviourPattern = null)
    {
        Transform spawnTransform = SpawnTransformGroups.GetRandomTransform(posGroupIndex);
        if (spawnTransform == null)
        {
            Debug.LogError($"몬스터 {monsterPrefab.name} 스폰 실패: 몬스터 스폰 위치 그룹 {posGroupIndex}에 유효한 스폰 위치가 없음");

            return null;
        }

        var monster = await objectSpawner.SpawnPoolObject(monsterPrefab, spawnTransform.position, spawnTransform.rotation) as MonsterObject;
        monster.Controller.SetBehaviourPattern(behaviourPattern);
        return monster;
    }

    public async UniTask<MonsterObject> SpawnNonPoolMonster(MonsterObjectData monsterPrefab, int posGroupIndex, BehaviourPatternSO behaviourPattern = null)
    {
        Transform spawnTransform = SpawnTransformGroups.GetRandomTransform(posGroupIndex);
        if (spawnTransform == null)
        {
            Debug.LogError($"몬스터 {monsterPrefab.name} 스폰 실패: 몬스터 스폰 위치 그룹 {posGroupIndex}에 유효한 스폰 위치가 없음");

            return null;
        }

        var monster = await objectSpawner.SpawnObject(monsterPrefab, spawnTransform.position, spawnTransform.rotation, null) as MonsterObject;
        monster.Controller.SetBehaviourPattern(behaviourPattern);
        return monster;
    }
}
