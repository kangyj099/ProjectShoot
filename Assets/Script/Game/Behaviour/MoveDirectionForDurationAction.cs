using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move Direction For Duration", story: "[Movement], [Direction] 방향으로 [Duration]초동안 이동", category: "Action", id: "ca12be5d771b7b2962c48a77f4f846b0")]
public partial class MoveDirectionForDurationAction : Action
{
    [SerializeReference] public BlackboardVariable<Movement> Movement;
    [SerializeReference] public BlackboardVariable<Vector2> Direction;
    [SerializeReference] public BlackboardVariable<float> Duration;

    [CreateProperty] private float m_Timer = 0.0f;
    protected override Status OnStart()
    {
        if (Movement.Value == null)
        {
            Debug.Log("무브먼트");
        }
        if (Direction.Value == null)
        {
            Debug.Log("디렉션");
        }


        Movement.Value.SetDirection(Direction.Value);

        m_Timer = Duration;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        m_Timer -= Time.deltaTime;
        if (m_Timer <= 0)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        Movement.Value.SetDirection(Vector2.zero);
    }
}

