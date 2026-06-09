public class ActorDieState : ActorStateBase
{
    public override ActorState State => ActorState.Die;

    public ActorDieState(ActorController controller) : base(controller) { }

    public override void OnEnter() { }
    public override void OnUpdate() { }
    public override void OnExit() { }
}
