using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int order;
    private Stack<UI_Popup> popupStack;
    private UI_Scene sceneUI;

    private GameObject root; // UI들을 묶어둘 최상위 빈 부모 객체

    private IAssetLoader assetLoader;

    public void Init()
    {
        order = UIDefine.canvasSortingOrder;
        popupStack = new Stack<UI_Popup>();
        sceneUI = null;

        assetLoader = new ResourcesLoader();
    }

public void Release()
    {
        ClearPopupUI();
        ClearSceneUI();
        order = UIDefine.canvasSortingOrder;
        assetLoader = null;
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = UI_Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // UI가 카메라에 상관없이 화면 맨 앞에 그려지도록 설정
        canvas.overrideSorting = true;

        if (sort) // 팝업 UI인 경우
        {
            canvas.sortingOrder = order; // 현재 order 값을 캔버스에 부여
            order++; // 다음 팝업을 위해 order 값을 1 증가
        }
        else // 씬 UI인 경우
        {
            canvas.sortingOrder = 0; // 정렬 우선순위를 0으로 고정
        }
    }

    public async UniTask<T> ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        string path = UIDefine.sceneUIPrefebsPath + name;
        GameObject prefab = await assetLoader.Load<GameObject>(path);

        if (prefab == null)
        {
            Debug.LogError($"[UIManager] 프리팹 불러오기 실패 {path}");
            return null;
        }

        GameObject go = Instantiate(prefab);
        T ui = UI_Utils.GetOrAddComponent<T>(go);
        sceneUI = ui;

        if (root == null) root = new GameObject { name = UIDefine.uiRootName };
        go.transform.SetParent(root.transform);

        return ui;
    }

    // [수정] UniTask 비동기 방식으로 변경된 스택형 팝업 UI 로드 및 생성
    public async UniTask<T> ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        string path = UIDefine.popupUIPrefebsPath + name;
        GameObject prefab = await assetLoader.Load<GameObject>(path);

        if (prefab == null)
        {
            Debug.LogError($"[UIManager] 프리팹 불러오기 실패 {path}");
            return null;
        }

        GameObject go = Instantiate(prefab);
        T popup = UI_Utils.GetOrAddComponent<T>(go);
        popupStack.Push(popup);

        if (root == null) root = new GameObject { name = UIDefine.uiRootName };
        go.transform.SetParent(root.transform);

        return popup;
    }

    // 화면 최상단 팝업을 닫는 함수
    public void ClosePopupUI()
    {
        if (popupStack.Count == 0) return; // 닫을 팝업이 스택에 없으면 무시

        UI_Popup popup = popupStack.Pop();
        Destroy(popup.gameObject);

        order--; // 다음 팝업이 뜰 때 순서가 꼬이지 않도록 order 값을 1 감소시켜 복구
    }

    // 모든 팝업 닫기
    public void ClearPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    // 씬UI 파괴
    public void ClearSceneUI()
    {
        if (sceneUI != null)
        {
            Destroy(sceneUI.gameObject);
            sceneUI = null;
        }
    }
}