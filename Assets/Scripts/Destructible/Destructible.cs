using UnityEngine;

namespace Bombsite
{
    public class Destructible : MonoBehaviour, IDestructible
    {
        [SerializeField]
        private DestructibleManagerAsset _destructibleManager;

        public bool Destructed { get; private set; } = false;

        public void Hit()
        {
            if (Destructed)
                return;

            Destruct();
        }

        private void Destruct()
        {
            Destructed = true;
            _destructibleManager.OnDestructed();
        }
    }
}
