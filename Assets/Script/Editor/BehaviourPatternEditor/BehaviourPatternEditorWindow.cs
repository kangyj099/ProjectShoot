using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourPatternEditorWindow : EditorWindow
{
    // 현재 편집중인 Pattern
    private BehaviourPatternSO curPattern = null;

    // 스크롤바
    Vector2 scroll = Vector2.zero;

    // 스탭 그리기 변수
    private int selectedStepIndex = -1;
    private bool isDragging;
    private int dragSourceIndex = -1;
    private int dragTargetIndex = -1;

    // Tools에 이 Window여는 버튼을 추가!
    [MenuItem("Tools/행동패턴 편집기")]
    public static void Open()
    {
        // 이 클래스 기반으로 Window를 연다!
        GetWindow<BehaviourPatternEditorWindow>("행동패턴 편집기");    // 탭 이름
    }

    private void OnGUI()
    {
        // 툴바 그리기
        DrawToolBar();

        // 편집중인 패턴이 없으면 패턴 선택 유도
        if (null == curPattern)
        {
            DrawDropArea();
            return;
        }
        DrawPatternInfo();

        // 스크롤 영역 컨텐츠
        scroll = EditorGUILayout.BeginScrollView(scroll);

        DrawSequence();

        EditorGUILayout.EndScrollView();
    }

    private void DrawToolBar()
    {
        // 툴바 스타일 레이아웃 수평 영역 배치(End로 닫아줘야함)
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

        // SO 선택 필드 추가
        curPattern = (BehaviourPatternSO)EditorGUILayout.ObjectField
            (
                curPattern, // 이 변수에 담기
                typeof(BehaviourPatternSO), // 타입 제한
                false   // 씬에 배치된 데이터 불허(Asset만 허용)
             );

        // + 버튼 추가. 동작 : 패턴 새로 만들기~
        if (GUILayout.Button("+", EditorStyles.toolbarButton))
        {
            //CreatePattern();
        }

        // Ping 버튼 추가
        if (null == curPattern)
        {
            if (GUILayout.Button("Ping"))
            {
                EditorGUIUtility.PingObject(curPattern);
            }
        }


        // 수평 영역 닫기
        EditorGUILayout.EndHorizontal();
    }

    private void DrawDropArea()
    {
        GUILayout.FlexibleSpace();
        // 수직 영역 시작 (End랑 쌍 해줘야함)
        GUILayout.BeginVertical("box");

        GUILayout.Label("Drop BehaviourPattern.asset");
        GUILayout.Space(30);
        var rect = GUILayoutUtility.GetRect(300, 100);
        GUI.Box(rect, "여기에 편집할 패턴 끌어다 놓기!");

        // 수직 영역 닫기
        GUILayout.EndVertical();

        GUILayout.FlexibleSpace();
    }

    private void DrawPatternInfo()
    {
        GUILayout.Space(10);
        // 패턴 이름
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("패턴 이름", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

            EditorGUI.BeginChangeCheck();
            string newName = EditorGUILayout.TextField(curPattern.Name);

            // 변경 감지하면 이름 갱신
            if (EditorGUI.EndChangeCheck())
            {
                curPattern.Name= newName;
                Save();
            }
        }
    }

    private void DrawSequence()
    {
        // Todo.모든 시퀀스 자유롭게 수정
        var sequence = curPattern.BasicSequences;

        // 시퀀스 없으면 안내박스 출력, Create버튼 출력
        if (null == sequence)
        {
            EditorGUILayout.HelpBox("시퀀스가 없어요", MessageType.Info);

            if (GUILayout.Button("Craete"))
            {
                AddSequence();
            }

            return;
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            string text = "{0} 시퀀스\n";
            if (sequence == curPattern.BasicSequences)
                text = string.Format(text, "기본");

            GUILayout.Label(text + sequence.Name);
            DrawStepList(sequence);
        }
    }

    #region 스탭 그리기
    private void DrawStepList(BehaviourSequence sequence)
    {
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < sequence.StepCount; i++)
        {
            DrawStep(sequence, i);
        }

        GUILayout.Space(5);

        if (GUILayout.Button("+스탭 추가"))
        {
            ShowAddMenu(sequence);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawStep(BehaviourSequence sequence, int index)
    {
        var step = sequence.GetStep(index);

        // 스탭
        using (new EditorGUILayout.VerticalScope("helpBox", GUILayout.MinWidth(100.0f), GUILayout.ExpandWidth(false)))
        {
            // 상단 라벨 + 삭제 버튼
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label($"[{index}]");

                if (GUILayout.Button(step.Type.ToString()))
                {
                    ShowChangeMenu(sequence, index);
                }

                if (GUILayout.Button("삭제", GUILayout.Width(70)))
                {
                    RemoveStep(sequence, index);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    return;
                }

            }

            // 스텝 인스펙터
            DrawStepInspector(step);
        }

        // 그려진 Rect를 가져와 드래그 핸들에 전달
        Rect rect = GUILayoutUtility.GetLastRect();
        HandleDrag(rect, index, sequence);

        // 드래그 중이면 시각적 표시
        if (isDragging && dragSourceIndex == index)
        {
            var prevColor = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, 0.5f);
            GUI.Box(rect, GUIContent.none);
            GUI.color = prevColor;
        }
    }

    private void DrawStepInspector(IBehaviourStep step)
    {
        if (null == step)
        { return; }

        // 스탭 타입
        var type = step.GetType();

        // 변수 필드 가져오기
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            DrawField(step, field);
        }
    }

    public void DrawField(object obj, FieldInfo field)
    {
        // 별도의 접근제한자 제한은 하지 않음
        object value = field.GetValue(obj);
        Type fieldType = field.FieldType;

        EditorGUI.BeginChangeCheck();

        object newValue = DrawFieldByType(field.Name, fieldType, value);

        if (EditorGUI.EndChangeCheck())
        {
            field.SetValue(obj, newValue);
            Save();
        }
    }

    public object DrawFieldByType(string name, Type fieldType, object value)
    {
        if (fieldType == typeof(int))
        {
            return EditorGUILayout.IntField(name, (int)value);
        }

        if (fieldType == typeof(float))
        {
            return EditorGUILayout.FloatField(name, (float)value);
        }

        if (fieldType == typeof(bool))
        {
            return EditorGUILayout.Toggle(name, (bool)value);
        }

        if (fieldType == typeof(string))
        {
            return EditorGUILayout.TextField(name, (string)value);
        }

        if (fieldType == typeof(Vector2))
        {
            return EditorGUILayout.Vector2Field(name, (Vector2)value);
        }

        if (fieldType == typeof(Vector3))
        {
            return EditorGUILayout.Vector3Field(name, (Vector3)value);
        }

        if (fieldType.IsEnum)
        {
            return EditorGUILayout.EnumPopup(
                name,
                (Enum)value);
        }

        if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType))
        {
            return EditorGUILayout.ObjectField(
                name,
                value as UnityEngine.Object,
                fieldType,
                false);
        }

        GUILayout.Label($"{name} ({fieldType.Name})");

        return value;
    }

    private void ShowChangeMenu(BehaviourSequence sequence, int index)
    {
        GenericMenu menu = new GenericMenu();

        foreach (BehaviourType type in Enum.GetValues(typeof(BehaviourType)))
        {
            if (type == BehaviourType.None)
                continue;

            menu.AddItem
                (
                new GUIContent(type.ToString()),
                false,
                () => { ChangeStep(sequence, index, type); }
                );
        }

        menu.ShowAsContext();
    }
    private void ChangeStep(BehaviourSequence sequence, int index, BehaviourType type)
    {
        var newStep = BehaviourPatternEditorFactory.Create(type);

        if (newStep == null)
            return;

        sequence.ReplaceStep(index, newStep);
        Save();
    }

    private void HandleDrag(Rect rect, int index, BehaviourSequence sequence)
    {
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.MouseDown:
                if (rect.Contains(e.mousePosition))
                {
                    dragSourceIndex = index;
                    GUI.changed = true;
                }
                break;
            case EventType.MouseDrag:
                if (dragSourceIndex != -1)
                {
                    isDragging = true;
                    Repaint();
                }
                break;
            case EventType.MouseUp:
                if (isDragging)
                {
                    int target = index;
                    if (target != dragSourceIndex)
                    {
                        sequence.MoveStep(dragSourceIndex, target);
                        Save();
                    }
                }

                dragSourceIndex = -1;
                isDragging = false;
                break;
        }

        if (isDragging && dragSourceIndex == index)
        {
            var prev = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, 0.25f);
            GUI.Box(rect, GUIContent.none);
            GUI.color = prev;
        }
    }
    #endregion

    private void AddSequence()
    {
        BehaviourSequence sequence = new();
        curPattern.AddSequence(sequence);
    }

    #region 스탭 추가하기
    private void ShowAddMenu(BehaviourSequence sequence)
    {
        GenericMenu menu = new GenericMenu();

        foreach (BehaviourType type in Enum.GetValues(typeof(BehaviourType)))
        {
            if (type == BehaviourType.None)
                continue;

            menu.AddItem
                (
                new GUIContent(type.ToString()),
                false, () => { AddStep(sequence, type); }
                );
        }

        menu.ShowAsContext();
    }

    private void AddStep(BehaviourSequence sequence, BehaviourType type)
    {
        var step = BehaviourPatternEditorFactory.Create(type);
        if (null == step)
            return;

        sequence.AddStep(step);

        Save();
    }
    #endregion

    private void RemoveStep(BehaviourSequence sequence, int index)
    {
        sequence.RemoveStep(index);
    }

    private void Save()
    {
        EditorUtility.SetDirty(curPattern);

        AssetDatabase.SaveAssets();

        Repaint();
    }
}
