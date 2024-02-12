using UnityEngine;
using DG.Tweening;

namespace Bombsite
{
    public class TileCanvas : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject TileContainer { get; private set; }

        [field: SerializeField]
        public GameObject TilePrefab { get; private set; }

        [Space, SerializeField]
        private CanvasGroup _canvasGroup;

        private Tween _fade;

        private void OnEnable() 
            => GameController.CountdownFinished += Hide;

        private void OnDisable()
            => GameController.CountdownFinished -= Hide;

        public void Hide() 
        {
            CursorManager.Instance.SetCursorDefault();
            _fade = _canvasGroup?.FadeOut(.5f);
        }

        private void OnDestroy()
            => _fade?.Kill();
    }
}
