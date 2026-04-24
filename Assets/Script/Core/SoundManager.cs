using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mainMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public Dictionary<string, AudioClip> bgmDictionary;
    public Dictionary<string, AudioClip> sfxDictionary;

    public void Init()
    {
        bgmDictionary = new Dictionary<string, AudioClip>();
        sfxDictionary = new Dictionary<string, AudioClip>();
        
        LoadSoundsFromResources(SoundConfig.BgmRoot, bgmDictionary);
        LoadSoundsFromResources(SoundConfig.SfxRoot, sfxDictionary);

        // BGM 중요도 설정
        bgmSource.priority = 0;
        sfxSource.priority = 128;

        LoadSettings();
    }

    public void Release()
    {
        // 딕셔너리 비우기 및 리소스 언로드
        bgmDictionary.Clear();
        sfxDictionary.Clear();
        Resources.UnloadUnusedAssets();
    }

    public void SetBgmVolume(float volume)
    {
        //볼륨을 0~1의 값으로 제한
        volume = Mathf.Clamp01(volume);

        // 0~1의 값을 -80dB ~ 0dB로 변환 (로그 스케일)
        float db = Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20f;
        mainMixer.SetFloat(SoundConfig.MixerBgmVol, db);

        PlayerPrefs.SetFloat(PlayerPrefsKeword.bgmVolume, volume);
    }

    public void SetSfxVolume(float volume)
    {        
        //볼륨을 0~1의 값으로 제한
        volume = Mathf.Clamp01(volume);

        // 0~1의 값을 -80dB ~ 0dB로 변환 (로그 스케일)
        float db = Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20f;
        mainMixer.SetFloat(SoundConfig.MixerSfxVol, db);

        PlayerPrefs.SetFloat(PlayerPrefsKeword.sfxVolume, volume);
    }

#region 재생/일시정지
    public void PlayBGM(string soundName)
    {
        if (bgmDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            if (bgmSource.clip == clip) return;
            bgmSource.clip = clip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{soundName}'을 찾을 수 없습니다.");
        }
    }

    public void PlaySFX(string soundName)
    {
        if (sfxDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            sfxSource.pitch = Random.Range(0.95f, 1.05f);
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{soundName}'을 찾을 수 없습니다.");
        }
    }

    public void PauseBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
    }

    public void ResumeBGM()
    {
        // 일시정지된 상태에서만 다시 재생하도록 처리
        if (!bgmSource.isPlaying)
        {
            bgmSource.UnPause();
        }
    }
#endregion

#region 로딩
    // 경로에 있는 모든 AudioClip을 읽어와 딕셔너리에 추가
    private void LoadSoundsFromResources(string path, Dictionary<string, AudioClip> dictionary)
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);
        foreach (AudioClip clip in clips)
        {
            if (!dictionary.ContainsKey(clip.name))
            {
                dictionary.Add(clip.name, clip);
            }
        }
        Debug.Log($"{path}에서 {clips.Length}개의 사운드를 로드했습니다.");
    }
    private void LoadSettings()
    {
        // 저장된 값이 없으면 기본값 0.75f 사용
        float bgmVol = PlayerPrefs.GetFloat(PlayerPrefsKeword.bgmVolume, 0.75f);
        float sfxVol = PlayerPrefs.GetFloat(PlayerPrefsKeword.sfxVolume, 0.75f);

        SetBgmVolume(bgmVol);
        SetSfxVolume(sfxVol);
    }

    #endregion
}