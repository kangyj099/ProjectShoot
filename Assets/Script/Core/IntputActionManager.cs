using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionManager
{
    private InputSystem_Actions inputActions;

    // 액션들
    private InputAction moveAction;
    private event Action<Vector2> OnMove;

    public void Init()
    {
        inputActions = new InputSystem_Actions();

        // 액션 맵 가져오기 (예: "Player"라는 이름의 액션맵)
        var playerMap = inputActions.Player;

        // 액션 등록
        moveAction = playerMap.Move;

        // 이벤트 연결
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;

        // 활성화
        moveAction.Enable();
    }

    public void Release()
    {
        // 이벤트 해제
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;

        // 비활성화
        moveAction.Disable();

        OnMove = null; // 외부 구독 해제
    }

    // 입력 이벤트 핸들러
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        OnMove?.Invoke(input);
        Debug.Log($"Move: {input}");
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(Vector2.zero); // 방향 초기화
        Debug.Log("Move canceled");
    }

    // 외부에서 Move 입력 이벤트 구독
    public void SubscribeToMoveInput(Action<Vector2> onMoveAction)
    {
        OnMove -= onMoveAction;    // 중복 방지
        OnMove += onMoveAction;
    }
    public void UnsubscribeFromMoveInput(Action<Vector2> onMoveAction)
    {
        OnMove -= onMoveAction;
    }
}