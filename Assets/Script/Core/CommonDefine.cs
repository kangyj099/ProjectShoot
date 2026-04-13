// 게임 전체에서 사용할 정의를 모아두는 스크립트
// define이 많아지면 해당 스크립트를 기능별로 나눠야 할 지도.

public enum GameState
{
    Init,       // 게임 초기화 (앱 켜짐)
    MainMenu,   // MainScene
    Loading,    // LoadingScene
    Playing    // GameScene
}

public enum SceneType
{
    Main,       // 0, 게임 시작 씬
    Loading,    // 1, 로딩씬
    Game,       // 2, 게임 플레이 씬
}
