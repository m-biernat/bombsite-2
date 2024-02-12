using UnityEngine;

namespace Bombsite
{
    public class DestroyBomb : MonoBehaviour
    {
        private void OnParticleSystemStopped()
            => Destroy(transform.parent.gameObject);
    }
}
