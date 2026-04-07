using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public sealed class GameRoot : SingletonMono<GameRoot>
{
    [SerializeField] private GameObject playerPrefab;

    private InputActionManager inputActionManager;

    PlayerController playerInstance;

    protected override void OnAwake()
    {
        inputActionManager = new InputActionManager();
        inputActionManager.Init();
        
        if (playerPrefab == null)
        {
            Debug.LogError("Player Object is not assigned in the inspector.\n인스펙터에 플레이어 오브젝트를 등록해주세요!");
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPlayer();

    }

    // Update is called once per frame
    void Update()
    {
              
    }

    private void SpawnPlayer()
    {
        var player = Instantiate<GameObject>(playerPrefab);
        player.transform.position = Vector3.zero; // 초기 위치 설정

        playerInstance = player.GetComponent<PlayerController>();
        playerInstance.Init(inputActionManager);
    }
}
