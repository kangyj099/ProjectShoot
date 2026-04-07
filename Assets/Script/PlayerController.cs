using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class PlayerController : MonoBehaviour
{
    private Movement movement;

    void Awake()
    {
        movement = gameObject.GetComponent<Movement>();
        if (movement == null)
        {
            Debug.LogError($"Movement component not found on {gameObject.name}.\n{gameObject} 객체에 Movement컴포넌트가 없습니다.");
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(InputActionManager inputActionManager)
    {
        InputSetting(inputActionManager);
    }

    void OnDestroy()
    {
    }

    private void InputSetting(InputActionManager inputActionManager)
    {
        inputActionManager.SubscribeToMoveInput(movement.SetDirection);
    }
}
