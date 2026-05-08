using UnityEngine;

public class Stage : MonoBehaviour
{
    [field: SerializeField]
    public MovementArea PlayerMovementArea { get; private set; }
    [field: SerializeField]
    public MovementArea MonsterMovementArea { get; private set; }
}
