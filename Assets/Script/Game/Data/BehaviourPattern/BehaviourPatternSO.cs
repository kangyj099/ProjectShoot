using UnityEngine;

public class BehaviourPatternSO : ScriptableObject
{
#if DEBUG
    public void TestSet()
    {
        Name = "테스트 패턴";
        sequences = new BehaviourSequence[1];
        sequences[0] = new BehaviourSequence();
        BasicSequences.TestSet();
    }

    public string GetSequenceName(int idx)
    {
        if (sequences == null || sequences.Length <= idx)
        {
            return null;
        }
        return sequences[idx].Name;
    }
#endif
    public string Name { get; private set; }

    BehaviourSequence[] sequences;
    public BehaviourSequence BasicSequences => sequences[0];
    public IBehaviourStep GetStep(int sequenceIndex, int stepIndex)
    {
        if (sequenceIndex < 0 || sequenceIndex >= sequences.Length)
            return null;
        BehaviourSequence sequence = sequences[sequenceIndex];
        if (stepIndex < 0 || stepIndex >= sequence.StepCount)
            return null;

        return sequence.GetStep(stepIndex);
    }

    void OnValidate()
    {
        if (sequences == null || sequences.Length == 0)
        {
            Debug.LogError($"BehaviourPatternSO {Name}의 sequences가 비어있습니다.");
        }
    }
}
