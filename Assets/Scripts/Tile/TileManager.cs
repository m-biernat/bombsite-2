using UnityEngine;

namespace Bombsite
{
    public class TileManager : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject TileContainer { get; private set; }

        [field: SerializeField]
        public GameObject TilePrefab { get; private set; }

        public void ShowGrid() => TileContainer?.SetActive(true);

        public void HideGrid() => TileContainer?.SetActive(false);
    }
}
