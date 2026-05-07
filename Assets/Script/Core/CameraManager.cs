using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera CinemachineCamera;
    [SerializeField] private CinemachineImpulseSource ImpulseSource;

    [Header("Default Camera Settings")]
    [Tooltip("기본 배율")]
    [SerializeField] private float defaultOrthographicSize = 5f;

    private Camera mainCamera;
    private Tweener zoomTweener;

    public void Init()
    {
        mainCamera = Camera.main;
        ApplyDefaultSettings();
    }

    private void ApplyDefaultSettings()
    {
        if (mainCamera != null)
        {
            mainCamera.orthographic = true;
            // 아래 두 줄은 임시 코드
            mainCamera.clearFlags = CameraClearFlags.SolidColor; // 잔상 지우기
            mainCamera.backgroundColor = Color.black; // 배경 검정
        }

        if (CinemachineCamera != null)
        {
            CinemachineCamera.Lens.OrthographicSize = defaultOrthographicSize;
        }
    }

    // 단순 진동
    public void ShakeCameraWithForce(float force)
    {
        if (ImpulseSource != null) ImpulseSource.GenerateImpulseWithForce(force);
    }


    /// 방향과 강도 있는 진동
    public void ShakeCamera(Vector3 velocity)
    {
        if (ImpulseSource != null) ImpulseSource.GenerateImpulse(velocity);
    }


    // DOTween을 사용하여 카메라 줌을 비동기로 제어
    public async UniTask ZoomToAsync(float targetSize, float duration, Ease ease = Ease.OutQuad)
    {
        if (CinemachineCamera == null) return;

        // 기존 트윈 제거 (중복 실행 방지)
        KillZoomTweener();

        // DOTween 생성
        zoomTweener = DOTween.To(
            () => CinemachineCamera.Lens.OrthographicSize, // getter
            x => {
                var lens = CinemachineCamera.Lens;
                lens.OrthographicSize = x;
                CinemachineCamera.Lens = lens;
            }, // setter
            targetSize,
            duration)
            .SetEase(ease) //가속도
            .SetUpdate(UpdateType.Normal) // 타임스케일 영향 받음
            .SetLink(gameObject); // 오브젝트 파괴 시 자동 제거

        // 트윈이 완료될 때까지 비동기 대기
        await zoomTweener.ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
    }

    // 돌려놓기
    public void ResetZoom(bool instant = true, float duration = 0.5f)
    {
        if (instant)
        {
            KillZoomTweener();
            if (CinemachineCamera != null)
            {
                var lens = CinemachineCamera.Lens;
                lens.OrthographicSize = defaultOrthographicSize;
                CinemachineCamera.Lens = lens;
            }
        }
        else
        {
            // Forget()을 사용하여 호출 직후 다음 로직 실행
            ZoomToAsync(defaultOrthographicSize, duration).Forget();
        }
    }

    private void KillZoomTweener()
    {
        if (zoomTweener != null && zoomTweener.IsActive())
        {
            zoomTweener.Kill();
        }
        zoomTweener = null;
    }

    public void Release()
    {
        KillZoomTweener();
    }

}