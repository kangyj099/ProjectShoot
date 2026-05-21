using UnityEngine;

public static class UI_Utils
{
    // 게임오브젝트에서 컴포넌트를 가져오되, 없다면 새로 부착해서 반환
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }

    // 이름(name)을 기반으로 자식 오브젝트/컴포넌트를 찾음
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);

                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    if (typeof(T) == typeof(GameObject)) return transform.gameObject as T;

                    T component = transform.GetComponent<T>();
                    if (component != null) return component;
                }
            }
        }
        else // 자식의 자식까지 모두 찾는 경우
        {
            return FindChildRecursive<T>(go.transform, name);
        }

        return null;
    }

    // 깊이 기반 탐색(DFS)
    public static T FindChildRecursive<T>(Transform parent, string name) where T : UnityEngine.Object
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (string.IsNullOrEmpty(name) || child.name == name)
            {
                if (typeof(T) == typeof(GameObject)) return child.gameObject as T;

                T component = child.GetComponent<T>();
                if (component != null) return component;
            }

            // 재귀 호출
            T grandchild = FindChildRecursive<T>(child, name);
            if (grandchild != null) return grandchild;
        }

        return null;
    }
}