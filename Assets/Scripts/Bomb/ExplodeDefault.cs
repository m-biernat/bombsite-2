using UnityEngine;

namespace Bombsite
{
    public class ExplodeDefault : MonoBehaviour, IExplode
    {
        [SerializeField]
        private ParticleSystem _explosionFx;

        [SerializeField]
        private float _delayOffset = .2f;
        
        private float _delay;

        private void Awake()
        {
            var detonate = GetComponent<IDetonate>();
            _delay = detonate.Delay - _delayOffset;
        }

        public void Invoke()
            => Invoke("Explode", _delay);

        private void Explode()
        {
            Instantiate(_explosionFx.gameObject, 
                        transform.position, 
                        _explosionFx.transform.rotation);
        }
    }
}
