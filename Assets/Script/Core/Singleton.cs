using UnityEngine;

// 
public class SingletonMonoDontDestroy<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
        DontDestroyOnLoad(gameObject);

        OnAwake();
    }

    protected virtual void OnDestroy()
    {
        OnDestroyed();

        if (Instance == this)
            Instance = null;
    }

    protected virtual void OnAwake() { }
    protected virtual void OnDestroyed() { }
}
