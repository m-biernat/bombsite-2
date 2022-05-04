using System;
using System.Collections;
using UnityEngine;

namespace Bombsite
{
    public class BombController : MonoBehaviour
    {
        public static event Action AllBombsPlanted;

        public static event Action AllBombsDetonated;

        public static event Action NoBombsDetonated;

        [SerializeField]
        private BombManagerAsset _bombManager;

        private IBomb _currBomb;

        private Tile _lastTile;

        private bool _detonating = false;

        private void OnEnable()
        {
            TileController.TileSelecting += OnTileSelecting;
            TileController.TileSelected += OnTileSelected;
            GameController.CountdownFinished += OnCountdownFinished;
        }

        private void OnDisable()
        {
            TileController.TileSelecting -= OnTileSelecting;
            TileController.TileSelected -= OnTileSelected;
            GameController.CountdownFinished -= OnCountdownFinished;
        }

        private void OnTileSelecting(Tile tile)
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

        private void OnTileSelected(Tile tile)
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

            if (!_detonating && 
                _bombManager.AllBombsUsed())
                OnAllBombsPlanted();
        }

        protected virtual void OnAllBombsPlanted() 
            => AllBombsPlanted?.Invoke();

        private void OnCountdownFinished()
        {
            _detonating = true;

            if (_currBomb != null)
                PlantBomb();
            
            if (_bombManager.NoBombsUsed())
                OnNoBombsDetonated();
            else
            {
                ActivateBombs();
                StartCoroutine(Detonate());
            }   
        }

        protected virtual void OnNoBombsDetonated()
            => NoBombsDetonated?.Invoke();

        private void ActivateBombs()
        {
            var gameObjects = 
                GameObject.FindGameObjectsWithTag("Bomb");

            foreach (var go in gameObjects)
                go?.GetComponent<IBomb>().Activate();
        }

        private IEnumerator Detonate() 
        { 
            foreach (var bomb in _bombManager.DetonableBombs)
            {
                bomb.Trigger();
                yield return new WaitForSeconds(.5f);
            }

            yield return new WaitForSeconds(.5f);

            OnAllBombsDetonated();
        }

        protected virtual void OnAllBombsDetonated()
            => AllBombsDetonated?.Invoke();
    }
}
