using UnityEngine;

public class MonsterController : ActorController
{
    BehaviourPatternRunner runner;

    protected override void OnAwake()
    {
        if (actorObject is MonsterObject monsterObject)
        {
            monsterObject.BindController(this);
        }
        else
        {
            Debug.LogError($"{name}에 MonsterController와 ActorObject가 있음. 컨트롤러를 ActorController로 변경하거나, BaseObject를 MonsterObject로 변경 필요.");
        }

        // 행동패턴 실행기 초기화
        runner = new BehaviourPatternRunner();
        runner.BindingMovement(movement);
    }

    protected override void OnUpdate()
    {
        if (runner != null)
        {
            runner.Update();
        }
    }

#if DEBUG
    public BehaviourPatternRunner GetRunner()
    {
        return runner;
    }
#endif
}