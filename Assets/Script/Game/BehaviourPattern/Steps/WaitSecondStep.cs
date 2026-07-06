using UnityEngine;

public class WaitSecondStep : BehaviourStep
{
    public override BehaviourType Behaviour => BehaviourType.WaitSecond;
    float remainSecond = .0f;
    float waitSecond = .0f;
    public WaitSecondStep(float waitSec)
    {
        waitSecond = waitSec;
    }

    protected override void OnEnter()
    {
        remainSecond = waitSecond;
    }

    protected override void OnUpdate()
    {
        remainSecond -= Time.deltaTime;
        if (remainSecond <= 0)
        {
            isCompleted = true;
        }
    }

    protected override void OnExit() { }
    protected override void OnReset() { }
}
