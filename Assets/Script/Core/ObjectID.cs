using UnityEngine;

public class ObjectID : MonoBehaviour
{
    [SerializeField] private string uniqueId;
    private int cachedHashCode;
    public string ID => uniqueId;

    // 에디터 스크립트에서 호출할 설정 함수
    public void SetId(string newId)
    {
        uniqueId = newId;
        cachedHashCode = uniqueId.GetHashCode();
    }
}