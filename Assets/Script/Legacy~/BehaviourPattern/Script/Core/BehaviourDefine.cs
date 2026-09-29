// 보관본: 원래 Assets/Script/Core/CommonDefine.cs 에 있던 행동 패턴 정의.
// 옛 행동 패턴 파이프라인을 Legacy~ 로 옮기면서 이 부분만 떼어 함께 보관한다.
// 원본 전체는 git 태그 legacy/behaviour-pattern 시점에서 볼 수 있다.

/// <summary>
/// 몬스터 행동 규칙
/// </summary>
public static class BehaviourConst
{
    public const int BASIC_SEQUENCE_INDEX = 0;
}

public enum BehaviourType
{
    None = -1,
    WaitSecond,     // 일정 시간 대기
    MovePoint,      // 특정 지점으로 이동
    MoveDirection,  // 특정 방향으로 (n초간)이동
    AttackOn,       // 공격 켜기
    AttackOff,      // 공격 끄기

    Count // enum 개수 세는 용도, 실제로는 사용하지 않음
}
