using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterTestScene : MonoBehaviour
{
    private const string behaviorText = "Step: {0}\r\nSequence: {1}\r\nStepIdx: {2}";
    
    public TextMeshProUGUI monsterBehaveText;
    public BehaviourPattern monsterBehaviour;

    public void Awake()
    {
        if (monsterBehaveText != null)
        {
            monsterBehaviour.StepTest();
            monsterBehaviour.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        monsterBehaveText.text = string.Format(behaviorText, monsterBehaviour.CurrentStepName, monsterBehaviour.CurrentSequenceIndex, monsterBehaviour.CurrentStepIndex);
    }
}
