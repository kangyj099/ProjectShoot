using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterTestScene : MonoBehaviour
{
    private const string behaviorText = "Step: {0}\r\nSequence: {1}\r\nStepIdx: {2}";
    
    public TextMeshProUGUI monsterBehaveText;
    public MonsterController monsterController;
    BehaviourPatternRunner behaviorPatternRunner;

    public void Awake()
    {
        if (monsterController)
        {
            behaviorPatternRunner = monsterController.GetRunner();
        }

        if (monsterBehaveText != null)
        {
            monsterBehaveText.text = string.Format(behaviorText, "N/A", "N/A", "N/A");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterBehaveText != null && behaviorPatternRunner != null)
        {
            monsterBehaveText.text = string.Format(behaviorText, ' ', behaviorPatternRunner.SequenceIndex, behaviorPatternRunner.StepIndex);
        }
    }
}
