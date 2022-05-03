using System;
using UnityEngine;

namespace Bombsite
{
    public class BombController : MonoBehaviour
    {
        public static event Action AllBombsPlanted;

        public static event Action AllBombsExploded;

        [SerializeField]
        private BombManagerAsset _bombManager;

        private IBomb _currBomb;

        private Tile _lastTile;

        private void OnEnable()
        {
            TileController.TileSelecting += OnTileSelecting;
            TileController.TileSelected += OnTileSelected;
        }

        private void OnDisable()
        {
            TileController.TileSelecting -= OnTileSelecting;
            TileController.TileSelected -= OnTileSelected;
        }

        protected virtual void OnTileSelecting(Tile tile)
        {   
            _lastTile = tile;
            
            if (_bombManager.TotalBombCount > 0)
                PrepareBomb();
        }

        private void PrepareBomb() 
        {
            var prefab = _bombManager.GetBombPrefab();

            if (prefab && prefab.CompareTag("Bomb"))
                SpawnBomb(prefab);
            else
                Debug.LogError("Bomb prefab is null or untagged", this);
        }

        private void SpawnBomb(GameObject prefab)
        {
            var position = _lastTile.transform.position;
            position.y += 1;
            var rotation = prefab.transform.rotation;

            var go = Instantiate(prefab, position, rotation);

            _currBomb = go?.GetComponent<IBomb>();

            if (_currBomb != null)
                _bombManager.RemoveFromContainer();
            else
            {
                Destroy(go);
                Debug.LogError("Bomb has no IBomb component", this);
            }
        }

        protected virtual void OnTileSelected(Tile tile)
        {
            _lastTile = tile;

            if (_currBomb != null)
                PlantBomb();
        }

        private void PlantBomb() 
        {
            _bombManager.RegisterBomb(_currBomb);
            _currBomb.Plant();
            _currBomb = null;

            _lastTile.Hide();

            if (_bombManager.TotalBombCount == 0)
                OnAllBombsPlanted();
        }

        protected virtual void OnAllBombsPlanted() 
            => AllBombsPlanted?.Invoke();

        protected virtual void OnAllBombsExploded()
            => AllBombsExploded?.Invoke();
    }
}
