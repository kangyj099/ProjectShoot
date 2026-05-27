public class ActorIdleState : IActorState
{
    public ActorState State => ActorState.Idle;
    public void OnEnter() { }
    public void OnUpdate() { }
    public void OnExit() { }
}
