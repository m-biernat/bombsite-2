using System;
using UnityEngine;

namespace Bombsite
{
    public class TileController : MonoBehaviour
    {
        private GameObject _currentGO;
        private Tile _currTile, _prevTile;

        private Camera _camera;

        [SerializeField] 
        private LayerMask _layerMask;

        public static event Action<Tile> TileSelecting;

        public static event Action<Tile> TileSelected;

        private void Start() => _camera = Camera.main;

        private void Update() => CheckForHovering();

        private void CheckForHovering()
        {
            if (InputManager.Instance.PointerState == PointerState.pressed)
                return;

            var go = InputManager.Instance.GetPointedObject(_camera, _layerMask);

            if (go.CompareTag("Tile") && go != _currentGO)
            {
                _prevTile = _currTile;
                _currTile = go?.GetComponent<Tile>();
                _currentGO = go;

                OnHovering();
            }
        }

        private void OnHovering() 
        { 
            _prevTile?.OnHovered();
            _currTile?.OnHovering();
        }

        private void OnEnable() 
        {
            InputManager.PointerPressing += OnPressing;
            InputManager.PointerPressed += OnPressed;
        }

        private void OnDisable() 
        {
            InputManager.PointerPressing -= OnPressing;
            InputManager.PointerPressed -= OnPressed;
        }

        protected virtual void OnPressing() 
        {
            _currTile?.OnPressing();
            TileSelecting?.Invoke(_currTile);
        }

        protected virtual void OnPressed()
        {
            _currTile?.OnPressed();
            TileSelected?.Invoke(_currTile);
        }
    }
}
