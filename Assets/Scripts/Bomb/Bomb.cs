using UnityEngine;

namespace Bombsite
{
    public class Bomb : MonoBehaviour, IBomb
    {
        public bool Active { get; private set; } = false;

        [field: SerializeField]
        public bool Detonable { get; private set; }

        [field: SerializeField]
        public bool Interactive { get; private set; }

        public int ID { get; private set; }

        [SerializeField, Space]
        private float _power = 14.0f; 
        
        [SerializeField]
        private float _radius = 3.0f,
                      _upForce = 1.0f,
                      _delay = 0.0f;

        [SerializeField]
        private LayerMask _layerMask;

        private bool _triggered = false;

        public void Activate() => Active = true;

        public void SetID(int id) => ID = id;

        public void Plant()
        {
            Debug.Log("Bomb has been planted");
        }

        public void Trigger()
        {
            if (_triggered)
                return;

            Debug.Log("Bomb has been triggered");
            Invoke("Detonate", _delay);
        }

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

                    if (bomb != null && bomb.Interactive)
                        bomb.Trigger();
                }

                if (go.CompareTag("Destructible"))
                    go?.GetComponent<IDestructible>()?.Hit();
            }

            Destroy(gameObject);
        }
    }
}
