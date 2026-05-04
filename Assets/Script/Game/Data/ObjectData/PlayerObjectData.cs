using UnityEngine;

[CreateAssetMenu(fileName = "PlayerObjectData", menuName = "Scriptable Object/Object Data/Player Object Data")]
public class PlayerObjectData : ObjectData
{
    [field: SerializeField] public int MaxHp { get; private set; }
    [field: SerializeField] public int MaxHpCap { get; private set; }
}
