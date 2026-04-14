using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

public class ResourcesLoader : IAssetLoader
{
    public async UniTask<T> Load<T>(string key) where T : Object
    {
        var request = await Resources.LoadAsync<T>(key);
        return request as T;    // 변환 실패하면 null
    }
}
