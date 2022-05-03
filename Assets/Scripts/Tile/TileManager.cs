using UnityEngine;

namespace Bombsite
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tileContainer;

        public GameObject TileContainer { get => _tileContainer; }

        [SerializeField]
        private GameObject _tilePrefab;

        public GameObject TilePrefab { get => _tilePrefab; }

        public void ShowGrid() => _tileContainer?.SetActive(true);

        public void HideGrid() => _tileContainer?.SetActive(false);
    }
}
