using UnityEngine;
using DG.Tweening;

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

        private Tween _sequence;

        private float _fadeTime = .25f;

        public override void OnLevelCompleted()
        {
            _gameUI.TextOnLevelCompleted.Show(0f, 0f, 
                () => _sequence = DOTween.Sequence()
                                .Insert(0, _gameUI.Controls.FadeOut(_fadeTime))
                                .Insert(0, _gameUI.ButtonsOnLevelCompleted.FadeIn(_fadeTime))
            );
        }

        public override void OnLevelFailed()
        {
            _gameUI.TextOnLevelFailed.Show(.25f, .5f,
                () => _sequence = DOTween.Sequence()
                                .Insert(0, _gameUI.Controls.FadeOut(_fadeTime))
                                .Insert(0, _gameUI.ButtonsOnLevelFailed.FadeIn(_fadeTime))
            );
        }

        private void OnDestroy() => _sequence.Kill();

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
                SceneLoader.Instance?.LoadEnding();
        }
    }
}
