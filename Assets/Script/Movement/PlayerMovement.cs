using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool _isDebugLogOn = false;
    [SerializeField] private float _moveSpeed = 5f;
    float MoveSpeed {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    private Rigidbody2D _rigidbody2D;
    private PlayerInput _playerInput;

    private void Awake()
    {
        // Essential component Setup
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (null == _rigidbody2D)
        {
            Debug.LogError("{PlayerMovement} Rigidbody2D component not found on the GameObject.\n게임오브젝트에 Rigidbody2D 컴포넌트가 없습니다.");
        }

        _playerInput = gameObject.GetComponent<PlayerInput>();
        if (null == _playerInput)
        {
            Debug.LogError("{PlayerMovement} PlayerInput component not found on the GameObject.\n게임오브젝트에 PlayerInput 컴포넌트가 없습니다.");
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

    public void OnMove(InputValue inputValue)
    {
        var vector2 = inputValue.Get<Vector2>();
        _rigidbody2D.linearVelocity = _moveSpeed * vector2;
        if (_isDebugLogOn)
        {
            Debug.Log($"{{PlayerMovement}} 방향입력값: {vector2}");
        }
    }
}
