using UnityEngine;
using DG.Tweening;

namespace Bombsite
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField]
        private float _target;

        private int _tweenId;

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
            DOTween.Play(_tweenId);
        }

        private void OnCountdownFinished()
        {
            _spriteRenderer.DOFade(0f, .5f)
                           .OnComplete(
                                () => gameObject.SetActive(false));
        }

        private void Animate()
        {
            _tweenId = transform.DOLocalMoveZ(_target, .5f)
                                .SetEase(_easeType)
                                .SetLoops(-1, LoopType.Yoyo).intId;
        }

        private void OnDisable()
        {
            GameController.CountdownFinished -= OnCountdownFinished;
            DOTween.Pause(_tweenId);
        }
    }
}
