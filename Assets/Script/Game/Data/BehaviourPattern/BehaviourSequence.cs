using UnityEngine;

public class BehaviourSequence
{
#if DEBUG
    public void TestSet()
    {
        Name = "테스트 시퀀스";
        steps = new IBehaviourStep[5];
        steps[0] = new WaitBehaviourStep() { Duration = 3f };
        steps[1] = new MoveDirectionBehaviourStep() { Direction = new Vector2(1,0), Duration = 1f };
        steps[2] = new MoveDirectionBehaviourStep() { Direction = new Vector2(1, 1), Duration = 1f };
        steps[3] = new WaitBehaviourStep() { Duration = 3f };
        steps[4] = new MoveDirectionBehaviourStep() { Direction = new Vector2(-1, -1), Duration = 2f };
    }
#endif
    public string Name { get; private set; }
    public int StepCount => steps.Length;
    public int CurrentStepIndex { get; private set; }

    IBehaviourStep[] steps;
    public IBehaviourStep GetStep(int index)
    {
        if (index < 0 || index >= steps.Length)
            return null;
        return steps[index];
    }
}
