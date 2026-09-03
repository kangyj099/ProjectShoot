using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnFormation))]
public class SpawnFormationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 Inspector 먼저 그리기
        DrawDefaultInspector();

        // 버튼 추가
        SpawnFormation script = (SpawnFormation)target;
        if (GUILayout.Button("자식 객체 Position으로 덮어쓰기"))
        {
            script.Positions.Clear();   // 기존 리스트 초기화

            foreach (Transform child in script.transform)
            {
                script.Positions.Add(child.localPosition);
            }

            // 변경 사항 저장
            EditorUtility.SetDirty(script);
        }
    }
}
