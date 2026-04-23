using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneLoadManager
{
    private int nextScene; //다음 씬
    private GameState nextState; //상태머신 다음 상태 정의

    public void Init()
    {
        nextScene = 0;
        nextState = GameState.MainMenu;
    }

    public void Release()
    {
        // 현재 특별히 처리할 내용은 없으나 형식 통일을 위한 함수
    }

    public void LoadScene(SceneType nextScene, GameState nextState)
    {
        this.nextScene = (int)nextScene;
        this.nextState = nextState;

        SceneManager.LoadScene((int)SceneType.Loading);
    }

    public async UniTask LoadTargetSceneAsync(Action<float> onProgressUpdate)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
        asyncOperation.allowSceneActivation = false; // 씬 로딩이 끝나면 자동으로 넘어갈 것인가

        // 로딩이 완료될 때까지 반복
        while (!asyncOperation.isDone)
        {
            float fProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // 0~0.9 값을 0~1로 보정

            onProgressUpdate?.Invoke(fProgress);

            if (asyncOperation.progress >= 0.9f)
            {
                // 프로그레스 바가 100% 채워지는 걸 보여주기 위한 대기 코드 (기획 요청으로 일단 주석처리)
                // await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

                // 씬 전환 처리
                asyncOperation.allowSceneActivation = true;
                GameRoot.Instance.GameStateManager.ChangeState(nextState);
            }

            // 다음 프레임까지 대기 (유니티 메인 스레드 멈춤 방지)
            await UniTask.Yield();
        }
    }
}
