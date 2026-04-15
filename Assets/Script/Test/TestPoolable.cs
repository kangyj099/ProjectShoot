using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TestPoolable : MonoBehaviour, IPoolable
{
    public IPool Pool { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    // IPooledObject 인터페이스 구현
    public void OnGet()
    {
        Debug.Log("TestPoolable OnGet");
        gameObject.SetActive(true);
    }
    public void OnRelease()
    {
        Debug.Log("TestPoolable OnRelease");
        gameObject.SetActive(false);
    }

    // 유니티 생명주기 메서드
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
