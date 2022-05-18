using UnityEngine;

namespace Bombsite
{
    public class PlantDefault : MonoBehaviour, IPlant
    {
        public void Invoke() => Plant();

        private void Plant()
        {
            var rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
    }
}
