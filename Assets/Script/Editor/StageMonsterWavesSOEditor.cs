using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageMonsterWavesSO))]
public class StageMonsterWavesSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StageMonsterWavesSO data = (StageMonsterWavesSO)target;
        if (GUILayout.Button("시간순으로 정렬하기"))
        {
            data.monsterWaves.Sort((a, b) => a.triggerTime.CompareTo(b.triggerTime));
            EditorUtility.SetDirty(data);
        }
    }
}