using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterWave
{
    public float triggerTime;
    public MonsterGroupSO monsterGroup;
    public BehaviourPatternSO pattern;
    // 진형
}

[CreateAssetMenu(fileName = "StageMonsterWavesSO", menuName = "Scriptable Object/Stage Monster Waves Data")]
public class StageMonsterWavesSO : ScriptableObject
{
    public List<MonsterWave> monsterWaves = new List<MonsterWave>();
}
