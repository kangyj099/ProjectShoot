using System;
using UnityEngine;
using Project.Movement;

namespace Project.Movement
{
    // 이동 잠궈야하는 상황 추가될 때 여기에 enum 추가
    [Flags]
    public enum LockType
    {
        None = 0,
        Dead = 1 << 0,
    };
};

public class Movement : MonoBehaviour
{

    [SerializeField] private bool isDebugLogOn = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 direction = Vector2.zero;

    private LockType moveLock = LockType.None;
    private LockType MoveLock { get => moveLock;
        set
        {
            moveLock = value;

#if UNITY_EDITOR
            moveLockText = value.ToString();
#endif
        }
    }

    private MoveArea movementArea;


    float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    public void SetArea(MoveArea area)
    {
        movementArea = area;
    }

    public void SetLock(Project.Movement.LockType lockType)
    {
        MoveLock |= lockType;
    }

    public void RemoveLock(LockType lockType)
    {
        MoveLock &= ~lockType;
    }

    private void Awake()
    {
        // Essential component Setup
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (MoveLock == LockType.None)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(direction * MoveSpeed * Time.fixedDeltaTime);
        AreaClamp();
    }

    public void TeleportPosition(Vector3 position)
    {
        transform.position = position;
        AreaClamp();
    }

    private void AreaClamp()
    {
        if (movementArea != null)
        {
            Vector3 clampedPosition = movementArea.Clamp(transform.position);
            transform.position = clampedPosition;
        }
    }


    // 인스펙터 표기를 위한 값들
    public string moveLockText = "None";
}
