using UnityEngine;

[CreateAssetMenu(fileName = "MonsterObjectData", menuName = "Scriptable Object/Object Data/Monster Object Data")]
public class MonsterObjectData : ObjectData
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int MaxHp { get; private set; }
}
