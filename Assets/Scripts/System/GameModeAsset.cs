using UnityEngine;

namespace Bombsite
{
    public abstract class GameModeAsset : ScriptableObject
    {
        public abstract void OnLevelCompleted();

        public abstract void OnLevelFailed();
        
        public abstract void LoadLevel(LevelAsset level);
        
        public abstract void ReloadLevel();
        
        public abstract void NextLevel();
    }
}
