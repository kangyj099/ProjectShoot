#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectID))]
public class ObjectIDEditor  : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate ID"))
        {
            var prefabId = (ObjectID)target;
            var guid = System.Guid.NewGuid().ToString();

            prefabId.SetId(System.Guid.NewGuid().ToString());
            EditorUtility.SetDirty(prefabId);

            Debug.Log($"{target.name}: 프리팹에 ID 할당 {guid}");
        }
    }
}
#endif