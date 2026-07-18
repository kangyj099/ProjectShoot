using UnityEngine;

public class BehaviourPatternSO : ScriptableObject
{
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
