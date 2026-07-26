using UnityEngine;

public class MoveDirectionBehaviourStep : BehaviourStep
{
    public override string Name => "방향 이동";
    public override BehaviourType Type => BehaviourType.MoveDirection;
    // 이동시간
    [SerializeField] float duration = 0.0f;
    // 이동 방향
    [SerializeField] Vector2 direction;
    // 이동 방향 노말 (편집기에서 넣은 수치를 실제 게임에서 사용할 normal벡터로 변환)
    public Vector2 Direction { get => direction.normalized; }

    public override void Start(BehaviourPatternRunner runner)
    {
        runner.SetDirection(Direction);
    }
    protected override void OnExecute(BehaviourPatternRunner runner)
    {

    }
    public override bool CheckStepComplete(BehaviourPatternRunner runner)
    {
        return runner.StepElapsedTime >= duration;
    }

    public override void Stop(BehaviourPatternRunner runner)
    {
        runner.SetDirection(Vector2.zero);
    }
}
