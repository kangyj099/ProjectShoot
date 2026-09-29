using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BehaviourSequence
{
    public string Name { get; private set; }
    public int StepCount => steps == null ? 0 : steps.Count;
    public int CurrentStepIndex { get; private set; }

    [SerializeReference]
    List<IBehaviourStep> steps;
    public void AddStep(IBehaviourStep step)
    {
        if (null == step)
            return;
        if (null == steps)
            steps = new();

        steps.Add(step);
    }
    public void RemoveStep(int index)
    {
        if (steps == null)
            return;

        if (steps.Count <= index || 0 > index)
            return;

        steps.RemoveAt(index);
    }
    public void MoveStep(int from, int to)
    {
        var step = steps[from];
        steps.RemoveAt(from);

        steps.Insert(to, step);
    }
    /// <summary>
    /// 스탭 교체
    /// </summary>
    /// <param name="index">교체할 인덱스</param>
    /// <param name="step">교체할 스탭</param>
    public void ReplaceStep(int index, IBehaviourStep step)
    {
        steps[index] = step;
    }

    public IBehaviourStep GetStep(int index)
    {
        if (index < 0 || index >= steps.Count)
            return null;

        return steps[index];
    }
}
