using UnityEngine;
using DG.Tweening;

namespace Bombsite.UI
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField]
        private float _target;

        private Tween _tween;

        [SerializeField]
        private Ease _easeType;

        private SpriteRenderer _spriteRenderer;

        private void Awake() 
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Animate();
        }

        private void OnEnable()
        {   
            GameController.CountdownFinished += OnCountdownFinished;
            DOTween.Play(_tween);
        }

        private void OnCountdownFinished()
        {
            _spriteRenderer.DOFade(0f, .5f)
                           .OnComplete(
                                () => gameObject.SetActive(false));
        }

        private void Animate()
        {
            _tween = transform.DOLocalMoveZ(_target, .5f)
                              .SetEase(_easeType)
                              .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            GameController.CountdownFinished -= OnCountdownFinished;
            DOTween.Pause(_tween);
        }

        private void OnDestroy() => _tween.Kill();
    }
}
