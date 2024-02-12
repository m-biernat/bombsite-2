using UnityEngine;

namespace Bombsite
{
    public class DetonateDefault : MonoBehaviour, IDetonate
    {
        [SerializeField]
        private float _power = 14.0f,
                      _radius = 3.0f,
                      _upForce = 1.0f;

        [field: SerializeField]
        public float Delay { get; private set; } = 0.4f;

        [SerializeField]
        private LayerMask _layerMask;

        private IImpulse _impulse;

        private Rigidbody _rigidbody;

        public void Invoke()
            => Invoke("Detonate", Delay);

        private void Awake()
        {
            _impulse = GetComponent<IImpulse>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Detonate()
        {
            var origin = transform.position;

            var objectsInRange = 
                Physics.OverlapSphere(origin, _radius, _layerMask);

            foreach (var go in objectsInRange)
            {
                if (go.CompareTag("Destructible"))
                    go?.GetComponent<IDestructible>()?.Hit();

                if (go.CompareTag("Bomb"))
                {
                    var bomb = go?.GetComponent<IBomb>();

                    if (bomb != null && bomb.IsTrigger)
                        bomb.Trigger();
                }
            }

            objectsInRange = 
                Physics.OverlapSphere(origin, _radius, _layerMask);

            foreach (var go in objectsInRange)
            {
                var rb = go?.GetComponent<Rigidbody>();

                if (rb)
                    rb.AddExplosionForce(_power, origin, _radius, 
                                         _upForce, ForceMode.Impulse);
            }

            _impulse?.Invoke();
            _rigidbody.isKinematic = true;            
        }
    }
}
