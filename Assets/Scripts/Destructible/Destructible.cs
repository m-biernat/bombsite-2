using UnityEngine;

namespace Bombsite
{
    public class Destructible : MonoBehaviour, IDestructible
    {
        [SerializeField]
        private DestructibleManagerAsset _destructibleManager;

        private bool _destructed = false;
        
        public bool Destructed { get => _destructed; }

        public void Hit()
        {
            if(_destructed)
                return;

            Destruct();
        }

        private void Destruct()
        {
            _destructed = true;
            _destructibleManager.OnDestructed();
        }
    }
}
