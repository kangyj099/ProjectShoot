using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 에셋 로더 인터페이스
/// Resources.Load와 Addressable Asset System에 대응하기 위해 인터페이스로 정의
/// </summary>
public interface IAssetLoader
{
    UniTask<T> Load<T>(string key) where T : Object;
}