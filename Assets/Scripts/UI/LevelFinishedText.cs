using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Bombsite.UI
{
    public class LevelFinishedText : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _textTransfrom;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private Sequence _sequence;

        [SerializeField]
        private float _moveTo;

        [SerializeField]
        private float _scaleTo;

        public void Show(float delayBefore, float delayAfter, Action onComplete = null)
        {
            _sequence = DOTween.Sequence();

            _sequence.AppendInterval(delayBefore);
            
            _sequence.Append(_canvasGroup.FadeIn(.4f));
            _sequence.AppendInterval(.2f);
            _sequence.Append(_canvasGroup.FadeOut(.4f));
            
            _sequence.Insert(delayBefore, 
                _textTransfrom.DOLocalMoveY(
                    20.0f, 
                    .3f)
            );

            _sequence.Insert(delayBefore, 
                _textTransfrom.DOScale(
                    Vector3.one,
                    .3f)
            );

            _sequence.AppendInterval(delayAfter);
            _sequence.OnComplete(() => onComplete?.Invoke());
        }

        private void OnDestroy() => _sequence.Kill();
    }
}
