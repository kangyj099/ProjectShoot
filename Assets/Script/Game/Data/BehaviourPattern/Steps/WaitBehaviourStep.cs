public class WaitBehaviourStep : BehaviourStep
{
    public override string Name => "대기";
    public override BehaviourType Type => BehaviourType.WaitSecond;
    // 대기 시간
    float duration = 0.0f;

    public override void Start(BehaviourPatternRunner runner)
    {
        runner.IsStepComplete = false;
    }
    protected override void OnExecute(BehaviourPatternRunner runner)
    {

    }
    public override bool CheckStepComplete(BehaviourPatternRunner runner)
    {
        return runner.StepElapsedTime >= duration;
    }
}
