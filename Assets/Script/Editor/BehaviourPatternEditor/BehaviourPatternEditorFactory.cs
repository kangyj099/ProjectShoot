using UnityEngine;

using static BehaviourType;

// 함수만 있으므로 static calss 선언
// (전역함수 단독 노출은 C#에선 불가능해서)
public static class BehaviourPatternEditorFactory
{
    /// <summary>
    /// 행동 패턴에 사용할 타입에 맞는 스탭을 생성
    /// </summary>
    /// <param type="행동 종류"></param>
    /// <returns>스탭 (스탭 생성 불가타입인 경우 null)</returns>
    public static IBehaviourStep Create(BehaviourType type)
    {
        switch (type)
        {
            case WaitSecond:// 일정 시간 대기
                {
                    return new WaitBehaviourStep();
                }
                break;
            case MovePoint:      // 특정 지점으로 이동
                { }
                break;
            case MoveDirection:  // 특정 방향으로 (n초간)이동
                {
                    return new MoveDirectionBehaviourStep();
                }
                break;

            case None:
            default:
                return null;
        }


        return null;
    }
}
