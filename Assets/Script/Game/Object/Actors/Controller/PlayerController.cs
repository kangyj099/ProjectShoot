using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class PlayerController : ActorController
{
    protected override void OnAwake()
    {
        base.OnAwake();

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

    public void SetMovementArea(MoveArea area)
    {
        if (area != null)
        {
            movement.SetArea(area);
        }
    }

    void OnDestroy()
    {
    }

    private void InputSetting(InputActionManager inputActionManager)
    {
        inputActionManager.SubscribeToMoveInput(movement.SetDirection);
    }
}
