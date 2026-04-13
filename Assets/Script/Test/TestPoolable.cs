using UnityEngine;

public class TestPoolable : MonoBehaviour, IPoolable
{
    public IPool Pool { get; set; }
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
}
