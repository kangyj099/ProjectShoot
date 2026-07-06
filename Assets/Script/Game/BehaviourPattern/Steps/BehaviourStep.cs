interface IBehaviourStep
{
    BehaviourType Behaviour { get; }
    bool IsCompleted { get; }
    bool IsCancelable { get; }

    void Enter();
    void Update();
    void Exit();
    void Reset();
}

public abstract class BehaviourStep : IBehaviourStep
{
    public abstract BehaviourType Behaviour { get; }

    protected bool isCompleted = false; // 스탭 완료 여부
    public bool IsCompleted => isCompleted;

    protected bool isCancelable = true;
    public bool IsCancelable => isCancelable;

    public virtual void Enter()
    {
        Reset();

        OnEnter();
    }
    public virtual void Update()    // TODO: Update는 MonoBehaviour의 Update와 혼동될 수 있으므로 이름 변경 고려
    {
        OnUpdate();
    }
    public virtual void Exit()
    {
        OnExit();
    }

    public void Reset()
    {
        isCompleted = false;

        OnReset();
    }

    protected abstract void OnEnter();
    protected abstract void OnUpdate();
    protected abstract void OnExit();
    protected abstract void OnReset();
}
