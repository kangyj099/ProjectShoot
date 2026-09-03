using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterWave
{
    public float triggerTime;
    public MonsterGroupSO monsterGroup;
    public BehaviourPatternSO pattern;
    public SpawnFormation formation;
}

[CreateAssetMenu(fileName = "StageMonsterWavesSO", menuName = "Scriptable Object/Stage Monster Waves Data")]
public class StageMonsterWavesSO : ScriptableObject
{
    public List<MonsterWave> monsterWaves = new List<MonsterWave>();

    private void OnValidate()
    {
        // Waves 유효성 검사
        for (int i = 0; i < monsterWaves.Count; i++)
        {
            var wave = monsterWaves[i];
            // 시간 검사
            if (wave.triggerTime < 0)
            {
                Debug.LogWarning($"MonsterWave at index {i} has a negative triggerTime. Setting it to 0.");
                wave.triggerTime = 0;
                monsterWaves[i] = wave;
            }

            // Wave 몬스터 수보다 포메이션 자리수가 적으면 문제
            if (wave.monsterGroup != null && wave.formation != null
                && wave.monsterGroup.MonsterCount > wave.formation.Count)
            {
                Debug.LogWarning($"MonsterWave at index {i} 몬스터 수가 포메이션 자리수보다 많습니다.");
            }
        }
    }
}
