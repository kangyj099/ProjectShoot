using UnityEngine;

public class Stage : MonoBehaviour
{
    [field: SerializeField]
    public MoveArea PlayerMovementArea { get; private set; }
    [field: SerializeField]
    public MoveArea MonsterMovementArea { get; private set; }
    [field: SerializeField]
    public SpawnTransformGroups MonsterSpawnTransformGroups { get; private set; }
}
