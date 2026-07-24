using UnityEngine;

public class BehaviourSequence
{
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
