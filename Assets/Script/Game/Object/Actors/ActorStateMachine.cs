using UnityEngine;

interface IActorState : IState
{
    ActorState State { get; }
}

public abstract class ActorStateBase : IActorState
{
    public abstract ActorState State { get; }

    protected ActorController Controller { get; private set; }
    public ActorStateBase(ActorController controller)
    {
        Controller = controller;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

}

public class ActorStateMachine : StateMachine<ActorState, ActorController>
{
    public ActorStateMachine(ActorController actor) : base(actor) { }

    public ActorState ActorState
    {
        get
        {
            return ((IActorState)currentState).State;
        }
    }

    public void Init()
    {
        states[ActorState.Idle] = CreateIdleState(owner);
        states[ActorState.Move] = CreateMoveState(owner);
        states[ActorState.Die] = CreateDieState(owner);

        // 초기 상태 Idle
        ChangeState(ActorState.Idle);
    }

    protected virtual IState CreateIdleState(ActorController controller)
    {
        return new ActorIdleState(controller);
    }
    protected virtual IState CreateMoveState(ActorController controller)
    {
        return new ActorMoveState(controller);
    }
    protected virtual IState CreateDieState(ActorController controller)
    {
        return new ActorDieState(controller);
    }
}
