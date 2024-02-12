using UnityEngine;
using DG.Tweening;

namespace Bombsite
{
    public class ExplodeDefault : MonoBehaviour, IExplode
    {
        [SerializeField]
        private Transform _hideOnExplode;

        [SerializeField]
        private ParticleSystem _explosionFx;

        [SerializeField]
        private float _delayOffset = .2f;

        [SerializeField]
        private float _hideOffset = .1f;
        
        private float _explosionDelay;

        private float _hideDelay;

        private Tween _tween;

        private void Awake()
        {
            var detonate = GetComponent<IDetonate>();
            _explosionDelay = detonate.Delay - _delayOffset;
            _hideDelay = _delayOffset - _hideOffset;
        }

        public void Invoke()
            => Invoke("Explode", _explosionDelay);

        private void Explode()
        {
            _explosionFx.Play();

            _tween = _hideOnExplode.DOScale(Vector3.zero, .1f)
                                   .SetDelay(_hideDelay);

            AudioManager.Instance.PlayEffect(AudioType.Explosion,
                                             transform.position);
        }

        private void OnDestroy() => _tween.Kill();
    }
}
