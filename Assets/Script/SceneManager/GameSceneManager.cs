using UnityEngine;

public class GameSceneManager : SingletonMono<GameSceneManager>
{
    [SerializeField] private GameObject playerPrefab;

    PlayerController playerInstance;
    public ObjectSpawner Spawner { get; private set; }

    private void Awake()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Object is not assigned in the inspector.\n인스펙터에 플레이어 오브젝트를 등록해주세요!");
            return;
        }

        PoolManager poolManager = new GameObject("PoolManager").AddComponent<PoolManager>();
        Spawner = new();
        Spawner.Init(poolManager);
    }

    private void Start()
    {
        SpawnPlayer();
    }

    protected override void OnDestroyed()
    {
    }

    private void SpawnPlayer()
    {
        var player = Instantiate<GameObject>(playerPrefab);
        player.transform.position = Vector3.zero; // 초기 위치 설정

        playerInstance = player.GetComponent<PlayerController>();
        playerInstance.Init(GameRoot.Instance.InputActionManager);
    }
}
