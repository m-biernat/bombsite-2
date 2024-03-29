using UnityEngine;

namespace Bombsite
{
    public class Destructible : MonoBehaviour, IDestructible
    {
        [SerializeField]
        private DestructibleManagerAsset _destructibleManager;

        public bool Destructed { get; private set; } = false;

        private void OnCollisionEnter(Collision other)
        {
            var go = other.gameObject;
            
            if (go.CompareTag("Destructible"))
                Hit();
        }

        public void Hit()
        {
            if (Destructed)
                return;

            Destruct();
        }

        protected virtual void Destruct()
        {
            Destructed = true;
            _destructibleManager.OnDestructed();
        }

        public void MarkUndamaged()
        {
            var renderer = GetComponent<Renderer>();

            if (renderer)
                foreach (var material in renderer.materials)
                    material?.SetInt("_Pulse", 1);
        }
    }
}
