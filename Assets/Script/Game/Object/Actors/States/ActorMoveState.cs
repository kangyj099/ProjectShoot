public class ActorMoveState : ActorStateBase
{
    public override ActorState State => ActorState.Move;

    public ActorMoveState(ActorController controller) : base(controller) { }

    public override void OnEnter() { }
    public override void OnUpdate() { }
    public override void OnExit() { }
}
