using UnityEngine;

public class WaitBehaviourStep : BehaviourStep
{
    public override string Name => "대기";
    public override BehaviourType Type => BehaviourType.WaitSecond;
    public override bool IsFixedStep => false;
    // 대기 시간
    [SerializeField] float duration = 0.0f;

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
