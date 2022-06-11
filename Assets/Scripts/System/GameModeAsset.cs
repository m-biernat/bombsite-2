using UnityEngine;
using Bombsite.UI;

namespace Bombsite
{
    public abstract class GameModeAsset : ScriptableObject
    {
        protected GameUI _gameUI;

        public void SetGameUI(GameUI gameUI) 
            => _gameUI = gameUI;

        public abstract void OnLevelCompleted();

        public abstract void OnLevelFailed();
        
        public abstract void LoadLevel(LevelAsset level);
        
        public abstract void ReloadLevel();
        
        public abstract void NextLevel();
    }
}
