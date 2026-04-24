using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSceneManager : SingletonMono<GameSceneManager>
{
    [SerializeField] private ObjectData playerObjectData;
    public PoolManager poolManager;
    PlayerController playerInstance;
    public ObjectSpawner Spawner { get; private set; }

    protected override void OnAwake()
    {
        if (playerObjectData == null)
        {
            Debug.LogError("Player Object Data is not assigned in the inspector.\n인스펙터에 플레이어 오브젝트 데이터를 등록해주세요!");
            return;
        }

        poolManager = new GameObject("PoolManager").AddComponent<PoolManager>();
        poolManager.gameObject.transform.SetParent(transform);
        if (poolManager == null)
        {
            Debug.LogError("PoolManager initialization failed.\nPoolManager 초기화에 실패했습니다.");
        }
        Spawner = new();
        Spawner.Init(poolManager);
        if (Spawner == null)
        {
            Debug.LogError("ObjectSpawner initialization failed.\nObjectSpawner 초기화에 실패했습니다.");
        }
    }

    private void Start()
    {
        SpawnPlayer().Forget();
    }

    protected override void OnDestroyed()
    {
    }

    private async UniTaskVoid SpawnPlayer()
    {
        BaseObject playerObj = await Spawner.SpawnObject(playerObjectData,  Vector3.zero, Quaternion.identity, transform);

        playerInstance = playerObj.GetComponent<PlayerController>();
        playerInstance.Init(GameRoot.Instance.InputActionManager);
    }
}
