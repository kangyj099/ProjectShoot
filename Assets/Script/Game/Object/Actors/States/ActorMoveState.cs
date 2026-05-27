public class ActorMoveState : IActorState
{
    public ActorState State => ActorState.Move;

    public void OnEnter() { }
    public void OnUpdate() { }
    public void OnExit() { }
}
