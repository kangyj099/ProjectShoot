using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;

public class SpawnFormation : MonoBehaviour
{
    [SerializeField] private List<Vector3> positions = new List<Vector3>();

    public List<Vector3> Positions => positions;
    public int Count => positions.Count;

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle(EditorStyles.helpBox);
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;

        Color oldBG = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        for (int i = 0; i < positions.Count; i++)
        {
            var worldPosition = transform.TransformPoint(positions[i]);
            //Gizmos.DrawCube(position, 0.5f * Vector3.one);
            Handles.Label(worldPosition, $"{i + 1}", style);
        }

        GUI.backgroundColor = oldBG;
    }


    //private void OnDrawGizmosSelected()
    //{
    //    Handles.color = new Color(1f, 1f, 1f, 0.5f);

    //    for (int i = 0; i < positions.Count; i++)
    //    {
    //        Vector3 worldPosition = transform.TransformPoint(positions[i]);

    //        EditorGUI.BeginChangeCheck();

    //        worldPosition = Handles.PositionHandle(worldPosition, Quaternion.identity);

    //        if (EditorGUI.EndChangeCheck())
    //        {
    //            Undo.RecordObject(this, "Move Spawn Position");

    //            positions[i] = transform.InverseTransformPoint(worldPosition);

    //            EditorUtility.SetDirty(this);
    //        }
    //    }
    //}
#endif
}
