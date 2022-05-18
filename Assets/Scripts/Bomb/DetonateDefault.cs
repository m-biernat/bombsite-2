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

        public void Invoke()
            => Invoke("Detonate", Delay);

        private void Detonate()
        {
            var origin = transform.position;

            var objectsInRange = 
                Physics.OverlapSphere(origin, _radius, _layerMask);

            foreach (var go in objectsInRange)
            {
                var rb = go?.GetComponent<Rigidbody>();

                if (rb)
                    rb.AddExplosionForce(_power, origin, _radius, 
                                         _upForce, ForceMode.Impulse);

                if (go.CompareTag("Bomb"))
                {
                    var bomb = go?.GetComponent<IBomb>();

                    if (bomb != null && bomb.IsTrigger)
                        bomb.Trigger();
                }

                if (go.CompareTag("Destructible"))
                    go?.GetComponent<IDestructible>()?.Hit();
            }

            Destroy(gameObject);
        }
    }
}
