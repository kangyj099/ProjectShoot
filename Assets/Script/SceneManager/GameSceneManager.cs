using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSceneManager : SingletonMono<GameSceneManager>
{
    public CameraManager CameraManager { get; private set; }

    [SerializeField] private ObjectData playerObjectData;
    [SerializeField] private Stage currentStage;
    [SerializeField] private Transform playerSpawnTransform;
    public PoolManager PoolManager { get; private set; }
    public ObjectSpawner Spawner { get; private set; }

    private PlayerController playerInstance;

    protected override void OnAwake()
    {
        CameraManager = GetComponentInChildren<CameraManager>();
        if (CameraManager == null) Debug.LogError("CameraManager를 찾을 수 없습니다!");
        else CameraManager.Init();

        if (playerObjectData == null)
        {
            Debug.LogError("Player Object Data is not assigned in the inspector.\n인스펙터에 플레이어 오브젝트 데이터를 등록해주세요!");
            return;
        }
        if (playerSpawnTransform == null)
        {
            Debug.LogError("Player Spawn Transform is not assigned in the inspector.\n인스펙터에 플레이어 스폰 트랜스폼을 등록해주세요!");
        }

        PoolManager = new GameObject("PoolManager").AddComponent<PoolManager>();
        PoolManager.gameObject.transform.SetParent(transform);
        if (PoolManager == null)
        {
            Debug.LogError("PoolManager initialization failed.\nPoolManager 초기화에 실패했습니다.");
        }
        Spawner = new();
        Spawner.Init(PoolManager);
        if (Spawner == null)
        {
            Debug.LogError("ObjectSpawner initialization failed.\nObjectSpawner 초기화에 실패했습니다.");
        }
    }

    protected override void OnDestroy()
    {
        CameraManager?.Release();
    }

    private void Start()
    {
        SetStage(currentStage);
        SpawnPlayer().Forget();
    }

    protected override void OnDestroyed()
    {
    }

    private async UniTaskVoid SpawnPlayer()
    {
        BaseObject playerObj = await Spawner.SpawnObject(playerObjectData, playerSpawnTransform.position, playerSpawnTransform.rotation, transform);

        playerInstance = playerObj.GetComponent<PlayerController>();
        playerInstance.Init(GameRoot.Instance.InputActionManager);
        playerInstance.SetMovementArea(currentStage.PlayerMovementArea);
    }

    private void SetStage(Stage stage)
    {
        currentStage = stage;

        playerInstance?.SetMovementArea(currentStage.PlayerMovementArea);
    }
}
