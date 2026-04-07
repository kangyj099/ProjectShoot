using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public sealed class GameRoot : SingletonMono<GameRoot>
{
    [SerializeField] private GameObject playerObject;

    protected override void OnAwake()
    {
        if (playerObject == null)
        {
            Debug.LogError("Player Object is not assigned in the inspector.\n인스펙터에 플레이어 오브젝트를 등록해주세요!");
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
              
    }
}
