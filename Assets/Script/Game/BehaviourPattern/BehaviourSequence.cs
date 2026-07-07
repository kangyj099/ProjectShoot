using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class BehaviourSequence
{
    [SerializeField] private IBehaviourStep[] steps;
    [SerializeField] private IBehaviourStep currentStep;
    public int CurrentStepIndex { get; private set; } = -1;
    public string CurrentStepName => currentStep != null ? currentStep.GetType().Name : "None";
    public bool IsCompleted { get; set; } = false;
    protected bool IsReservedExit { get; set; } = false;
    public int StepCount => steps.Length;

    //////////////////
    ///For Test&Debug
#if DEBUG
    public void StepTest()
    {
        if (steps == null)
        {
            steps = new IBehaviourStep[5];
            for (int i = 0; i < steps.Length; i++)
            {
                steps[i] = new WaitSecondStep(5.0f);
            }

            CurrentStepIndex = 0;
            currentStep = steps[CurrentStepIndex];
        }
    }
#endif
    ///For Test&Debug
    //////////////////


    public BehaviourSequence()
    {
        if (steps == null || steps.Length == 0)
        {
            return;
        }
        // 첫 스탭 시작(스텝 초기화 불필요하므로 Reset() 호출하지 않음)
        CurrentStepIndex = 0;
        currentStep = steps[CurrentStepIndex];
    }

    public void Activate()
    {
        // 행동 스탭이 있으면 첫 스탭 시작 세팅
        if (steps == null || steps.Length == 0)
            return;

        currentStep.Enter();
    }

    /// <summary>
    /// 다음 스탭으로 넘어감
    /// (ReserveExit 호출, CurrentStepIndex 증가, currentSequence 변경)
    /// * IsCompleted가 true인 경우에만 호출되어야 함. 내부에서 확인 안 함
    /// </summary>
    private void NextStep()
    {
        if (steps == null || steps.Length == 0)
            return;

        currentStep.Exit();

        CurrentStepIndex++;
        if (CurrentStepIndex >= steps.Length)
        {
            currentStep = null; // 루프가 아닌 시퀀스인 경우에 스탭 종료
            IsCompleted = true;
            return;
        }

        currentStep = steps[CurrentStepIndex];
        currentStep.Enter();
    }

    public void OnUpdate()
    {
        if (currentStep == null || IsCompleted) return;

        currentStep.Update();
        if (currentStep.IsCompleted == true)
        {
            // 종료 예약일 때는 종료로 뺌
            if (IsReservedExit)
            {
                IsCompleted = true;
                return;
            }

            NextStep();
        }
    }

    public void ReserveExit()
    {
        IsReservedExit = true;

        if (currentStep == null)
        {
            IsCompleted = true;
            return;
        }

        if (currentStep.IsCancelable)
        {
            currentStep.Exit();
            currentStep = null;
            CurrentStepIndex = -1;
        }
    }

    public void Reset()
    {
        foreach (var step in steps)
        {
            step.Reset();
        }
        IsReservedExit = false;
        IsCompleted = false;
        CurrentStepIndex = 0;
        currentStep = steps[CurrentStepIndex];
    }
}
