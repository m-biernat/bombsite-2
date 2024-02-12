using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Game Manager", 
                     menuName = "Bombsite/Game Manager")]
    public class GameManagerAsset : ScriptableObject
    {
        public GameModeAsset GameMode { get; private set; }

        [field: SerializeField]
        public GameModeAsset DefaultGameMode { get; private set; }

        public void Init() 
        { 
            GameController.LevelCompleted += OnLevelCompleted;
            GameController.LevelFailed += OnLevelFailed;
            GameMode = DefaultGameMode;
        }

        private void OnLevelCompleted() => GameMode?.OnLevelCompleted();

        private void OnLevelFailed() => GameMode?.OnLevelFailed();

        public void LoadLevel(LevelAsset level) => GameMode?.LoadLevel(level);

        public void ReloadLevel() => GameMode?.ReloadLevel();

        public void NextLevel() => GameMode?.NextLevel();

        public void LoadMenu() => SceneLoader.Instance?.LoadMenu();

        public void QuitGame() => SceneLoader.Instance?.QuitGame();
    }
}
