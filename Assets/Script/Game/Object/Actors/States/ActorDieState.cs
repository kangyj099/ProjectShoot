public class ActorDieState : IActorState
{
    public ActorState State => ActorState.Die;

    public void OnEnter() { }
    public void OnUpdate() { }
    public void OnExit() { }
}
