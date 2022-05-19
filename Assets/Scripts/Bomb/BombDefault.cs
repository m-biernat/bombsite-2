using UnityEngine;

namespace Bombsite
{
    public class BombDefault : MonoBehaviour, IBomb
    {
        public bool Active { get; private set; } = false;

        [field: SerializeField]
        public bool Registerable { get; private set; }

        [field: SerializeField]
        public bool IsTrigger { get; private set; }

        public int ID { get; private set; }

        private bool _triggered = false;

        private IPlant _plant;

        private IDetonate _detonate;

        private IExplode _explode;

        private BombLabel _label;

        private void Awake()
        {
            _plant = GetComponent<IPlant>();
            _detonate = GetComponent<IDetonate>();
            _explode = GetComponent<IExplode>();
            _label = GetComponent<BombLabel>();
        }

        public void Activate() => Active = true;

        public void SetID(int id) => ID = id;

        public void Plant() 
        {
            if (Registerable)
                _label?.Init(ID);

            _plant?.Invoke();
        }

        public void Trigger()
        {
            if (_triggered)
                return;

            _detonate?.Invoke();
            _explode?.Invoke();
        }
    }
}
