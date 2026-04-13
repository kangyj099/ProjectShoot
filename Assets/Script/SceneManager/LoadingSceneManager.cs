using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar; // 테스트용. 추후 수정

    private void Start()
    {
        // 초기화
        progressBar.value = 0f;

        // 비동기 로드 실행
        GameRoot.Instance.SceneLoadManager.LoadTargetSceneAsync(UpdateProgress).Forget();
    }

    private void UpdateProgress(float progress)
    {
        progressBar.value = progress;
    }
}