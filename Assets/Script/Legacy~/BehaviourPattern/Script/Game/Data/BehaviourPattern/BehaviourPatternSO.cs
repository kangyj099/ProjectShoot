using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourPatternData", menuName = "Scriptable Object/Behaviour Pattern Data")]
public class BehaviourPatternSO : ScriptableObject
{
#if DEBUG
    public string GetSequenceName(int idx)
    {
        if (sequences == null || sequences.Count <= idx)
        {
            return null;
        }
        return sequences[idx].Name;
    }
#endif
    public string Name { get; set; }

    [SerializeField] private List<BehaviourSequence> sequences = new();
    public BehaviourSequence BasicSequences => null == sequences || 0 == sequences.Count ? null : sequences[BehaviourConst.BASIC_SEQUENCE_INDEX];
    bool isBasicLoop = true;
    public bool IsBasicLoop { get => isBasicLoop; private set { isBasicLoop = value; } }
    public IBehaviourStep GetStep(int sequenceIndex, int stepIndex)
    {
        if (sequenceIndex < 0 || sequenceIndex >= sequences.Count)
            return null;
        BehaviourSequence sequence = sequences[sequenceIndex];
        if (stepIndex < 0 || stepIndex >= sequence.StepCount)
            return null;

        return sequence.GetStep(stepIndex);
    }

    public void AddSequence(BehaviourSequence sequence)
    {
        if (null == sequence)
            return;
        if (null == sequences)
            sequences = new();

        sequences.Add(sequence);
    }
    public void RemoveSequence(int index)
    {
        if (sequences == null)
            return;

        if (sequences.Count <= index || 0 > index)
            return;

        sequences.RemoveAt(index);
    }
    public void MoveSequence(int from, int to)
    {
        var sequence = sequences[from];
        sequences.RemoveAt(from);

        sequences.Insert(to, sequence);
    }
    /// <summary>
    /// 스탭 교체
    /// </summary>
    /// <param name="index">교체할 인덱스</param>
    /// <param name="sequence">교체할 스탭</param>
    public void ReplaceSequence(int index, BehaviourSequence sequence)
    {
        sequences[index] = sequence;
    }


    void OnValidate()
    {
        if (sequences == null || sequences.Count == 0)
        {
            Debug.LogError($"BehaviourPatternSO {Name}의 sequences가 비어있습니다.");
        }
    }
}
