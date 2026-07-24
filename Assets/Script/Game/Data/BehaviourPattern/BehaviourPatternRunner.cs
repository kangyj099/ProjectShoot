using System;
using UnityEngine;

public class BehaviourPatternRunner
{
    private const int INVALID_INDEX = -1;

    BehaviourPatternSO pattern = null;
    public int SequenceIndex { get; private set; } = INVALID_INDEX;
    public int StepIndex { get; private set; } = INVALID_INDEX;
    public bool IsStepComplete { get; set; }

    private IBehaviourStep currentStep = null;
    private IBehaviourStep PickCurrentStep() => pattern?.GetStep(SequenceIndex, StepIndex);

    public Action<Vector2> SetDirection { get; private set; }

    private float stepStartTime;
    public float StepElapsedTime => Time.time - stepStartTime;

    #region 정보값 바인딩
    public void BindingMovement(Movement movement)
    {
        SetDirection = movement.SetDirection;
    }

    public void SetPattern(BehaviourPatternSO pattern)
    {
        this.pattern = pattern;
    }
    #endregion

    public void Update()
    {
        if (currentStep == null)
            return;

        currentStep.Execute(this);

        if (IsStepComplete)
        {
            NextStep();
        }
    }

    public void Reset()
    {
        pattern = null;

        SequenceIndex = INVALID_INDEX;
        StepIndex = INVALID_INDEX;

        stepStartTime = Time.time;
    }

    private void StepReady()
    {
        stepStartTime = Time.time;
        IsStepComplete = false;
    }

    private void NextStep()
    {
        StepIndex++;
        currentStep = PickCurrentStep();

        StepReady();
        currentStep?.Start(this);
    }
}
