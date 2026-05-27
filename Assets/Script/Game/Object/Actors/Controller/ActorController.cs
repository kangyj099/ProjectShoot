using UnityEngine;

public class ActorController : MonoBehaviour
{
    protected Movement movement;

    void Awake()
    {
        movement = gameObject.GetComponent<Movement>();

        OnAwake();
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    protected virtual void OnAwake() { }
}
