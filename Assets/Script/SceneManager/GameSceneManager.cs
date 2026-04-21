using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSceneManager : SingletonMono<GameSceneManager>
{
    [SerializeField] private ObjectData playerObjectData;

    PlayerController playerInstance;
    public ObjectSpawner Spawner { get; private set; }

    private void Awake()
    {
        if (playerObjectData == null)
        {
            Debug.LogError("Player Object Data is not assigned in the inspector.\n인스펙터에 플레이어 오브젝트 데이터를 등록해주세요!");
            return;
        }

        PoolManager poolManager = new GameObject("PoolManager").AddComponent<PoolManager>();
        Spawner = new();
        Spawner.Init(poolManager);
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
