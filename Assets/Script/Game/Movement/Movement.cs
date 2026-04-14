using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private bool isDebugLogOn = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 direction = Vector2.zero;

    [SerializeField] private bool moveLock = false;

    private Rigidbody2D rigidbody2D;

    float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    private void Awake()
    {
        // Essential component Setup
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (null == rigidbody2D)
        {
            Debug.LogError("{PlayerMovement} Rigidbody2D component not found on the GameObject.\n게임오브젝트에 Rigidbody2D 컴포넌트가 없습니다.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        rigidbody2D.transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }
}
