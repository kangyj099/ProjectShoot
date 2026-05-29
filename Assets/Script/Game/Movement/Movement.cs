using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private bool isDebugLogOn = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 direction = Vector2.zero;

    [SerializeField] private bool moveLock = false;

    private MovementArea movementArea;

    float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    public void SetArea(MovementArea area)
    {
        movementArea = area;
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
        if (!moveLock)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(direction * MoveSpeed * Time.fixedDeltaTime);
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
}
