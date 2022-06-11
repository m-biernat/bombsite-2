using UnityEngine;
using DG.Tweening;

namespace Bombsite.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private GameManagerAsset _gameManager;

        [SerializeField, Space]
        private CanvasGroup _onCountdownFinished;

        [field: SerializeField, Space]
        public LevelFinishedText TextOnLevelCompleted { get; private set; }

        [field: SerializeField]
        public LevelFinishedText TextOnLevelFailed { get; private set; }

        [field: SerializeField, Space]
        public CanvasGroup ButtonsOnLevelCompleted { get; private set; }
        
        [field: SerializeField]
        public CanvasGroup ButtonsOnLevelFailed { get; private set; }

        [field: SerializeField, Space]
        public CanvasGroup Controls { get; private set; }

        private Tween _fade;

        private void Awake()
            => _gameManager?.GameMode?.SetGameUI(this);

        private void OnEnable()
            => GameController.CountdownFinished += OnCountdownFinished;

        private void OnDisable()
            => GameController.CountdownFinished -= OnCountdownFinished;

        private void OnCountdownFinished() 
            => _fade = _onCountdownFinished?.FadeOut(.5f);

        private void OnDestroy() => _fade?.Kill();
    }
}
