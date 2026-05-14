using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
}

public abstract class StateMachine<TKey, TOwner> where TKey : System.Enum
{
    protected Dictionary<TKey, IState> states = new Dictionary<TKey, IState>();
    protected IState currentState;
    protected TOwner owner; // 이걸 통해 특정 플레이어나 몬스터 개체를 참조

    public StateMachine(TOwner owner)
    {
        this.owner = owner;
    }

    public void ChangeState(TKey newState)
    {
        if (!states.ContainsKey(newState)) return;

        Debug.Log($"상태 머신 동작: {currentState} -> {newState}");
        currentState?.OnExit();
        currentState = states[newState];
        currentState.OnEnter();
    }

    public void StateUpdate()
    {
        currentState?.OnUpdate();
    }

    public virtual void Release()
    {
        states.Clear();
        currentState = null;
    }
}