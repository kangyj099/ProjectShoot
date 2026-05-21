using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> objectsDictionary = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();
    private void Awake() => Init();
    private void OnDestroy() => Release();

    // Enum에 정의된 이름들을 기반으로 자식 오브젝트들을 찾아 딕셔너리에 바인딩
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        objectsDictionary[typeof(T)] = objects;

        for (int i = 0; i < names.Length; i++)
        {
            objects[i] = UI_Utils.FindChildRecursive<T>(gameObject.transform, names[i]);

            if (objects[i] == null) Debug.LogWarning($"[UI_Base] 바인딩하지 못했습니다! : {names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        if (objectsDictionary.TryGetValue(typeof(T), out UnityEngine.Object[] objects))
            return objects[idx] as T;

        return null;
    }

    protected TextMeshProUGUI GetText(int idx) => Get<TextMeshProUGUI>(idx);
    protected Button GetButton(int idx) => Get<Button>(idx);
    protected Image GetImage(int idx) => Get<Image>(idx);

    public static void BindEvent(GameObject gameObject, Action<PointerEventData> action, UIEvent type = UIEvent.Click)
    {
        // 대상 게임오브젝트에 UI_EventHandler 컴포넌트가 없으면 추가
        UI_EventHandler evt = UI_Utils.GetOrAddComponent<UI_EventHandler>(gameObject);

        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action; // 중복 방지
                evt.OnClickHandler += action;
                break;
            case UIEvent.Drag:
                evt.OnDragHandler -= action;  // 중복 방지
                evt.OnDragHandler += action;
                break;
        }
    }

    public static void UnbindEvent(GameObject gameObject, Action<PointerEventData> action, UIEvent type = UIEvent.Click)
    {
        if (gameObject == null) return;

        UI_EventHandler evt = gameObject.GetComponent<UI_EventHandler>();
        if (evt == null) return;

        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                break;
            case UIEvent.Drag:
                evt.OnDragHandler -= action;
                break;
        }
    }

    public virtual void Release()
    {
        // 캐싱된 모든 GameObject나 컴포넌트를 돌며 핸들러 정리
        foreach (var objArray in objectsDictionary.Values)
        {
            if (objArray == null) continue;

            foreach (var obj in objArray)
            {
                if (obj == null) continue;

                // 오브젝트가 GameObject면 그대로 쓰고, 컴포넌트면 gameObject를 추출
                GameObject go = obj as GameObject;
                if (go == null && obj is Component comp)
                {
                    go = comp.gameObject;
                }

                if (go != null)
                {
                    UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
                    if (evt != null)
                    {
                        evt.ClearAllEvents();
                    }
                }
            }
        }

        // 딕셔너리 메모리 연결 끊기
        objectsDictionary.Clear();
    }
}

public class UI_Popup : UI_Base
{
    public override void Init() => GameRoot.Instance.UIManager.SetCanvas(gameObject, true);
    public virtual void ClosePopup() => GameRoot.Instance.UIManager.ClosePopupUI();
}

public class UI_Scene : UI_Base
{
    public override void Init() => GameRoot.Instance.UIManager.SetCanvas(gameObject, false);
}