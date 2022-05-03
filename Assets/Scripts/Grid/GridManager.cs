using UnityEngine;
using System.Collections.Generic;

namespace Bombsite
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tileContainer;

        [SerializeField]
        public GameObject _tilePrefab;

        public GameObject TilePrefab { get => _tilePrefab; }

        [Space]

        [SerializeField]
        private List<Tile> _tiles;

        public void ShowGrid() => _tileContainer?.SetActive(true);

        public void HideGrid() => _tileContainer?.SetActive(false);

        public void AddTile(Tile tile)
        {
            _tiles?.Add(tile);
            tile.transform.SetParent(_tileContainer.transform, true);
        }

        public void RemoveTile(Tile tile)
        {
            _tiles?.Remove(tile);
            tile.transform.SetParent(null, true);
        }

        public bool IsTileInContainer(Tile tile)
            => tile.transform.IsChildOf(_tileContainer.transform);

        public bool IsTileListed(Tile tile)
            => _tiles.Exists(_tile => tile);

        public void RemoveUnavailableTiles()
            => _tiles.RemoveAll(_tile => _tile is null);
    }
}
