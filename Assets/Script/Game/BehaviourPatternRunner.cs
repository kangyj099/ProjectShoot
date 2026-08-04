using System;
using UnityEngine;

public class BehaviourPatternRunner
{
#if DEBUG
    public string GetCurSeqName => pattern.GetSequenceName(SequenceIndex);
    public string GetCurStepName => currentStep.Name;
#endif

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
        SequenceIndex = 0;
        StepIndex = 0;
        currentStep = PickCurrentStep();
    }
    #endregion

    public void Update()
    {
        if (currentStep == null)
            return;
        if (!currentStep.IsFixedStep)
        {
            currentStep.Execute(this);
        }

        if (IsStepComplete)
        {
            NextStep();
        }
    }

    public void FixedUpdate()
    {
        if (currentStep.IsFixedStep)
        {
            currentStep.Execute(this);
        }

        lastPos = GetPosition();
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
        // 루프 시퀀스인 경우 인덱스 끝까지 갔을 때 초기화함
        if (SequenceIndex == BehaviourConst.BASIC_SEQUENCE_INDEX
            && pattern.BasicSequences.StepCount <= StepIndex
            && true == pattern?.IsBasicLoop)
        {
            StepIndex = 0;
        }

        currentStep.Stop(this);

        currentStep = PickCurrentStep();

        StepReady();
        currentStep?.Start(this);
    }
}
