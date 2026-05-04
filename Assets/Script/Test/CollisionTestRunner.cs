using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollisionTestRunner : MonoBehaviour
{
    public MonsterObjectData testMonsterData;
    public ObjectData testItemData;

    void Start()
    {

    }

    public void MonsterSpawn()
    {
        GameSceneManager.Instance.Spawner.SpawnObject(testMonsterData, new Vector3(0, 3, 0), Quaternion.identity, null).Forget();
    }
}
