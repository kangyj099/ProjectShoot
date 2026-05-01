using UnityEngine;

/// <summary>
/// <fileName>생성되는 에셋 파일 기본 이름</fileName>
/// <menuName>프로젝트 우클릭 메뉴 Create 카테고리 어느부분에서 어떤 이름으로 찾을 수 있는지</menuName>
/// </summary>
[CreateAssetMenu(fileName = "TestObjectData", menuName = "Scriptable Object/Test/TestObjectData", order = 1)]
public class TestObjectData : ObjectData
{
    public Sprite sprite;
}
