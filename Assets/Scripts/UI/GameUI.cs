using UnityEngine;
using DG.Tweening;

namespace Bombsite.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _onCountdownFinished;

        private Tween _fade;

        private void OnEnable()
        {
            GameController.CountdownFinished += OnCountdownFinished;
        }

        private void OnDisable()
        {
            GameController.CountdownFinished -= OnCountdownFinished;
        }

        private void OnCountdownFinished() 
            => _fade = _onCountdownFinished?.Fade(0f, .5f, false);

        private void OnDestroy() => _fade?.Kill();
    }
}
