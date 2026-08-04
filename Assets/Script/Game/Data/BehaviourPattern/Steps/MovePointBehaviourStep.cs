using UnityEngine;

public class MovePointBehaviourStep : BehaviourStep
{
    public override string Name => "목적지 이동";
    public override BehaviourType Type => BehaviourType.MovePoint;
    public override bool IsFixedStep => true;
    // 목적지
    [SerializeField] Vector2 destination;
    public Vector2 Destination => destination;

    public override void Start(BehaviourPatternRunner runner)
    {
        var direction = (Destination - runner.GetPosition()).normalized;
        runner.SetDirection(direction);
    }
    protected override void OnExecute(BehaviourPatternRunner runner)
    {

    }
    public override bool CheckStepComplete(BehaviourPatternRunner runner)
    {
        Vector2 curPos = runner.GetPosition();
        if (curPos == runner.LastPos)
        {
            runner.StepStuckElapsed += Time.fixedDeltaTime;
            if (runner.StepStuckElapsed > 0.2f)
            {
                // 이동이 멈춘 상태로 0.2초 이상 지속되면 막힌 것으로 판단
                return true;
            }
        }
        else
        {
            runner.StepStuckElapsed = 0.0f;
        }
        // 목적지에 도달했는지 확인 (여긴 거의 안 걸릴듯)
        if (curPos == destination)
        {
            return true;
        }

        // 각 pos에서 목적지로 향하는 방향벡터를 구함
        Vector2 CurPosToDest = Destination - curPos;
        Vector2 LastPosToDest = Destination - runner.LastPos;
        /* 내적하면 x*x + y * y라서 두 벡터가 같은방향이면 항상 양수가 나옴. 
         역이 100 % 성립하진 않음.목표를 향해 정확히 직선으로 갈 때 유효
         이동 경로 직선과 목적지 점 사이의 거리가 1회이동량보다 작아지면 부정확해짐
         이 경우 거리계산식 기반으로 변경 필요 */
        if (Vector2.Dot(CurPosToDest, LastPosToDest) <= 0)
        {
            return true;
        }

        return false;
    }

    public override void Stop(BehaviourPatternRunner runner)
    {
        runner.SetDirection(Vector2.zero);
    }
}
