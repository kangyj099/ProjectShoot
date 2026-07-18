using UnityEngine;

// TODO. 스탭 데이터 런타임에 수정 불가하게 set private로 잠그기
// 편집기 데이터 밀어넣기를 위해서 임시값 경유 클래스 제작 후 Copy 함수 호출하여 값 넣기
// (SerializeField로 해결되면 그렇게 하기)

/// <summary>
/// 스탭 행동을 정의하는 인터페이스
/// </summary>
public interface IBehaviourStep
{
    string Name { get; }
    BehaviourType Type { get; }
    // 스탭 동작에 필요한 초기값 주입
    void Start(BehaviourPatternRunner runner);
    // 스탭 동작 수행시킴
    void Execute(BehaviourPatternRunner runner);
    // 현재 runner 상태가 step 완료 조건을 만족하는지 확인하고 runner.IsStepComplete를 true로 설정
    bool CheckStepComplete(BehaviourPatternRunner runner);
    // 스탭 동작 종료 값 정리 시킴
    void Stop(BehaviourPatternRunner runner);
}

/// <summary>
/// 스탭
/// 런타임 객체 아님. 여러 객체가 공유하는 데이터. PatterunSO에 종속된 데이터임
/// 런타임 데이터는 각 Actor의 BehaviourPatternRunner에서 소유하고, Step은 Runner 실행 근거를 제공하는 정적 데이터
/// 
/// ※주의※__스탭 데이터를 변수 접근해서 수정하면, 이 스탭 데이터를 공유하는 다른 actor의 행동에도 영향이 감!!!
/// </summary>
public abstract class BehaviourStep : IBehaviourStep
{
    public abstract string Name { get; }
    public abstract BehaviourType Type { get; }
    public abstract void Start(BehaviourPatternRunner runner);
    public virtual void Execute(BehaviourPatternRunner runner)
    {
        OnExecute(runner);
        CheckStepComplete(runner);
    }

    // 스텝별 수행시킬 동작을 구현
    protected virtual void OnExecute(BehaviourPatternRunner runner) { }

    public abstract bool CheckStepComplete(BehaviourPatternRunner runner);

    public virtual void Stop(BehaviourPatternRunner runner) { }

}
