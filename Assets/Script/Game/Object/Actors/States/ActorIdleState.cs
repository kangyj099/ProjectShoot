public class ActorIdleState : ActorStateBase
{
    public override ActorState State => ActorState.Idle;
    public ActorIdleState(ActorController controller) : base(controller) { }

    public override void OnEnter() { }
    public override void OnUpdate() { }
    public override void OnExit() { }
}
