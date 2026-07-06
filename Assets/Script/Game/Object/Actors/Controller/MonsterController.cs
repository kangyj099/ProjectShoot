using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : ActorController
{
    [SerializeField] BehaviourPattern behaviourPattern;

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
    }
}
