using System;
using UnityEditor;
using UnityEngine;

public class PrefabIdPostprocessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            if (assetPath.EndsWith(".prefab"))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (prefab != null)
                {
                    if (!prefab.TryGetComponent<ObjectID>(out var prefabId))
                    {
                        prefabId = prefab.AddComponent<ObjectID>();
                        prefabId.SetId(System.Guid.NewGuid().ToString());
                        EditorUtility.SetDirty(prefab);
                        Debug.Log($"{assetPath}: 프리팹에 ID 할당 {prefabId.ID}");
                    }
                }
            }
        }
    }
}
