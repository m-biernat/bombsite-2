using UnityEngine;

namespace Bombsite
{
    public class MoreDestructible : Destructible
    {
        [SerializeField]
        private GameObject _spawnablePrefab;

        protected override void Destruct()
        {
            base.Destruct();

            Instantiate(_spawnablePrefab,
                        transform.position,
                        transform.rotation);

            gameObject.SetActive(false);
        }
    }
}
