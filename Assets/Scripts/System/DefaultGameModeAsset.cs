using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Default Game Mode", 
                     menuName = "Bombsite/Default Game Mode")]
    public class DefaultGameModeAsset : GameModeAsset
    {
        [SerializeField]
        private CurrentLevelAsset _currentLevel;

        [SerializeField]
        private LevelManagerAsset _levelManager;

        public override void OnLevelCompleted()
        {
            Debug.Log("CLEAR");
        }

        public override void OnLevelFailed()
        {
            Debug.Log("FAILURE");
        }

        public override void LoadLevel(LevelAsset level)
            => SceneLoader.Instance?.LoadLevel(level);

        public override void ReloadLevel()
            => SceneLoader.Instance?.LoadLevel(_currentLevel.Info.Asset);

        public override void NextLevel()
        {
            var index = _levelManager.GetIndexOfLevelInfo(_currentLevel.Info) + 1;
            var nextLevel = _levelManager.GetLevelInfo(index)?.Asset;
            
            if (index > 0 && nextLevel)
                SceneLoader.Instance?.LoadLevel(nextLevel);
            else
                SceneLoader.Instance?.LoadMenu();
        }
    }
}
