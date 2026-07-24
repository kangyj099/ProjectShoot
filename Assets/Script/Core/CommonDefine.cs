// 게임 전체에서 사용할 정의를 모아두는 스크립트
// define이 많아지면 해당 스크립트를 기능별로 나눠야 할 지도.

#region 씬
public enum GameState
{
    Init,       // 초기화
    MainMenu,
    Loading,
    Playing,
    Pause,      // 일시정지

    Count // enum 개수 세는 용도, 실제로는 사용하지 않음
}

public enum SceneType
{
    Main,       // 0, 게임 시작 씬
    Loading,    // 1, 로딩씬
    Game,       // 2, 게임 플레이 씬

    Count // enum 개수 세는 용도, 실제로는 사용하지 않음
}
#endregion

#region 오브젝트
public enum ObjectType
{
    None = -1,
    Player,     // Actor
    Monster,    // Actor
    Projectile,
    Item,
    Claw,

    Count // enum 개수 세는 용도, 실제로는 사용하지 않음
}

public enum ActorState
{
    None = -1,
    Idle,
    Move,
    Die,

    Count // enum 개수 세는 용도, 실제로는 사용하지 않음
}

/// <summary>
/// 몬스터 행동 규칙
/// </summary>
public static class BehaviourConst
{
    public const int BASIC_SEQUENCE_INDEX = 0;
}

public enum BehaviourType
{
    None = -1,
    WaitSecond,     // 일정 시간 대기
    MovePoint,      // 특정 지점으로 이동
    MoveDirection,  // 특정 방향으로 (n초간)이동
    AttackOn,       // 공격 켜기
    AttackOff,      // 공격 끄기

    Count // enum 개수 세는 용도, 실제로는 사용하지 않음
}

public enum ItemType
{

}

public enum CollisionType
{
    None = -1,
    Damage,
    Buff,
    Debuff,
    Capture,

    Count // enum 개수 세는 용도, 실제로는 사용하지 않음
}
#endregion

#region 스킬
public static class SkillConfig
{
    public const float MAX_SKILL_SPEED = float.MaxValue;
    public const float MAX_SKILL_FIRERATE = float.MaxValue;
    public const int MAX_SKILL_SHOTCOUNT = 3;
    public const float MAX_SKILL_SHOTINTERVAL = float.MaxValue;
}
#endregion

#region UI
public enum UIEvent
{
    Click,
    Drag,

    count
}

public static class UIDefine
{
    public const int canvasSortingOrder = 10;
    public const string uiRootName = "UI_Root";
    public const string sceneUIPrefebsPath = "Prefabs/UI/Scene/";
    public const string popupUIPrefebsPath = "Prefabs/UI/Popup/";
}
#endregion

#region playerprefs
public static class PlayerPrefsKeword
{
    public const string bgmVolume = "BgmVolume";
    public const string sfxVolume = "SfxVolume";
}
#endregion

#region 사운드
public static class SoundConfig
{
    public const string BgmRoot = "Sounds/BGM";
    public const string SfxRoot = "Sounds/SFX";

    public const string MixerBgmVol = "BgmVol";
    public const string MixerSfxVol = "BgmVol";
}
#endregion