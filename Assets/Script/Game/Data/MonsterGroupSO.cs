using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterGroupSO", menuName = "Scriptable Object/Monster Group Data")]
public class MonsterGroupSO : ScriptableObject
{
    /// <summary>
    /// 몬스터 그룹에 포함된 몬스터들의 리스트
    /// ※ 0번이 "대장"
    /// </summary>
    public List<MonsterObjectData> monsterList = new List<MonsterObjectData>();
    public int MonsterCount => monsterList?.Count ?? 0;
}
