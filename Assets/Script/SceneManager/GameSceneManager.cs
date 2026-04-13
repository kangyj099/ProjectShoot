using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private InputActionManager inputActionManager;

    PlayerController playerInstance;

    private void Awake()
    {
        inputActionManager = new InputActionManager();
        inputActionManager.Init();

        if (playerPrefab == null)
        {
            Debug.LogError("Player Object is not assigned in the inspector.\n인스펙터에 플레이어 오브젝트를 등록해주세요!");
            return;
        }
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void OnDestroy()
    {
        inputActionManager?.Release();
    }

    private void SpawnPlayer()
    {
        var player = Instantiate<GameObject>(playerPrefab);
        player.transform.position = Vector3.zero; // 초기 위치 설정

        playerInstance = player.GetComponent<PlayerController>();
        playerInstance.Init(inputActionManager);
    }
}
