using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SettingsPopup : UI_Popup
{
    // 하이얼라키의 이름과 일치해야 함
    enum Buttons
    {
        BackspaceButton
    }

    enum Sliders
    {
        BGMVolumeSlider,
        SFXVolumeSlider
    }

    public override void Init()
    {
        base.Init(); // 부모(UI_Popup)의 캔버스 설정 실행

        // 컴포넌트 바인딩
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        // 초기값 설정 (SoundManager 또는 PlayerPrefs에서 가져옴)
        // Master는 기존 매니저에 없으므로 기본값 처리
        Get<Slider>((int)Sliders.BGMVolumeSlider).value = PlayerPrefs.GetFloat(PlayerPrefsKeword.bgmVolume, 0.75f);
        Get<Slider>((int)Sliders.SFXVolumeSlider).value = PlayerPrefs.GetFloat(PlayerPrefsKeword.sfxVolume, 0.75f);

        BindEvent(GetButton((int)Buttons.BackspaceButton).gameObject, OnClickClose);

        Get<Slider>((int)Sliders.BGMVolumeSlider).onValueChanged.AddListener(OnBgmVolumeChanged);
        Get<Slider>((int)Sliders.SFXVolumeSlider).onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    // 슬라이더 값 변경 콜백들
    private void OnBgmVolumeChanged(float value) => GameRoot.Instance.SoundManager.SetBgmVolume(value);
    private void OnSfxVolumeChanged(float value) => GameRoot.Instance.SoundManager.SetSfxVolume(value);

    // 닫기 버튼 콜백
    private void OnClickClose(PointerEventData eventData)
    {
        ClosePopup();
    }

    public override void Release()
    {
        // 슬라이더 리스너 제거
        if (objectsDictionary.ContainsKey(typeof(Slider)))
        {
            Get<Slider>((int)Sliders.BGMVolumeSlider).onValueChanged.RemoveListener(OnBgmVolumeChanged);
            Get<Slider>((int)Sliders.SFXVolumeSlider).onValueChanged.RemoveListener(OnSfxVolumeChanged);
        }

        // 명시적 작성
        UnbindEvent(GetButton((int)Buttons.BackspaceButton).gameObject, OnClickClose);

        base.Release();
    }
}