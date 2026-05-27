using UnityEngine;

interface IActorState: IState
{
    ActorState State { get; }
}

public class ActorStateMachine : StateMachine<ActorState, ActorController>
{
    public ActorStateMachine(ActorController actor) : base(actor) { }

    public ActorState ActorState { get
        {
            return ((IActorState)currentState).State;
        }
    }

    public void Init()
    {
        states[ActorState.Idle] = CreateIdleState();
        states[ActorState.Move] = CreateMoveState();
        states[ActorState.Die] = CreateDieState();

        // 초기 상태 Idle
        ChangeState(ActorState.Idle);
    }

    protected virtual  IState CreateIdleState()
    {
        return new ActorIdleState();
    }
    protected virtual IState CreateMoveState()
    {
        return new ActorMoveState();
    }
    protected virtual IState CreateDieState()
    {
        return new ActorDieState();
    }
}
