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

        private void OnEnable() 
            => GameController.CountdownFinished += Hide;

        private void OnDisable()
            => GameController.CountdownFinished -= Hide;

        public void Hide() 
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0f, .5f)
                        .OnComplete(
                            () => TileContainer?.SetActive(false));

            CursorManager.Instance.SetCursorDefault();
        }
    }
}
