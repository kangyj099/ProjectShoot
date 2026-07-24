using UnityEngine;

public class MoveDirectionBehaviourStep : BehaviourStep
{
    public override string Name => "방향 이동";
    public override BehaviourType Type => BehaviourType.MoveDirection;
    // 이동시간
    public float Duration { get; set; }
    // 이동 방향
    public Vector2 Direction { get; set; }

    public override void Start(BehaviourPatternRunner runner)
    {
        runner.SetDirection(Direction);
    }
    protected override void OnExecute(BehaviourPatternRunner runner)
    {

    }
    public override bool CheckStepComplete(BehaviourPatternRunner runner)
    {
        return runner.StepElapsedTime >= Duration;
    }

    public override void Stop(BehaviourPatternRunner runner)
    {
        runner.SetDirection(Vector2.zero);
    }
}
