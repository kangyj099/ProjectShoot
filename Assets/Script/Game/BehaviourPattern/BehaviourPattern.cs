using Unity.VisualScripting;
using UnityEngine;

public class BehaviourPattern : MonoBehaviour
{
    [SerializeField] private MonsterController actor;

    [SerializeField] private bool isBasicLoop = false;  // 기본 시퀀스 루프 여부
    public bool IsBasicLoop => isBasicLoop;
    // 인덱스 순서가 priority임. 0 기본, 나머지 interrupt.
    // 높은순 진행중일 때 낮은순 interrupt X. 트리거 동시에 들어올시 정방향 순회 후 먼저 걸린거로 ㄱ
    [SerializeField] private BehaviourSequence[] sequence;
    // 기본 시퀀스는 sequence[0]에 위치 고정
    private BehaviourSequence BasicSequence => (sequence != null ? sequence[0] : null);
    [SerializeField] private BehaviourSequence currentSequence;
    [SerializeField] private int reserveSequenceIdx = -1;

    //////////////////
    ///For Test&Debug
#if DEBUG
    public int CurrentSequenceIndex => (sequence != null ? System.Array.IndexOf(sequence, currentSequence) : -1);
    public string CurrentStepName => (currentSequence != null ? currentSequence.CurrentStepName : "None");
    public int CurrentStepIndex => (currentSequence != null ? currentSequence.CurrentStepIndex : -1);

    public void StepTest()
    {
        if (null == sequence || sequence.Length <= 0)
        {
            sequence = new BehaviourSequence[1];
            sequence[0] = new BehaviourSequence();
            sequence[0].StepTest();
        }
    }
#endif
    ///For Test&Debug
    //////////////////


    private void Awake()    // 오브젝트 최초 활성화시 동작
    {
        if (sequence != null)
        {
            enabled = false;
        }
    }

    private void OnEnable()
    {
        if (sequence == null)   // 시퀀스가 없으면 활성화 안 되도록 처리
        {
            enabled = false;
        }
    }

    void Start()    // 컴포넌트 최초 활성화시 동작
    {
        if (BasicSequence != null && BasicSequence.StepCount > 0)
        {
            currentSequence = BasicSequence;    // 기본 시퀀스 시작
            currentSequence.Activate();
        }
    }

    void Update()
    {
        ProcessReserveChangeSequence(); // 한 번의 시퀀스 업데이트를 보장하기 위해 앞에 둠

        if (currentSequence == null)
        {
            return;
        }

        currentSequence.OnUpdate();

        if (currentSequence.IsCompleted)
        {
            BehaviourSequence nextSequence = null;
            if (IsBasicLoop && currentSequence == BasicSequence)
            {
                nextSequence = BasicSequence;    // 기본 시퀀스 루프
            }

            ChangeSequence(nextSequence);    // nextSequence == null 가능(동작없음)
        }

    }

    /// <summary>
    /// 추후 구현
    /// </summary>
    private void InterruptSequence()
    {
    }

    /// <summary>
    /// (현재는 InterruptSequence()에서만 사용)
    /// </summary>
    private void ReserveChangeSequence(int reserveSequenceIdx)
    {
        currentSequence.ReserveExit();
        this.reserveSequenceIdx = reserveSequenceIdx;
    }

    /// <summary>
    /// 시퀀스 변경 예약 처리
    /// reserveSequenceIdx를 예약 여부 판단 플래그로 사용
    /// </summary>
    private void ProcessReserveChangeSequence()
    {
        if (reserveSequenceIdx < 0)
        {
            return;
        }

        ChangeSequence(sequence[reserveSequenceIdx]);
    }

    /// <summary>
    /// 시퀀스 즉시 바꾸기(IsCompleted가 true인 경우에만 호출되어야 함. 내부에서 체크 안 함)
    /// nextSequence == null 가능(동작없음)
    /// </summary>
    private bool ChangeSequence(BehaviourSequence nextSequence)
    {
        if (currentSequence != null)
        {
            if (false == currentSequence.IsCompleted)
            {
                return false;
            }

            currentSequence.Reset();    // 종료시에만 초기화 (이어하기를 염두)
        }

        currentSequence = nextSequence;
        return true;
    }
}
