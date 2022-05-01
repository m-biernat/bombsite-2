using UnityEngine;

namespace Bombsite
{
    public class TileSelectionController : MonoBehaviour
    {
        private GameObject _currentGO;
        private GridTile _currTile, _prevTile;

        private Camera _camera;

        [SerializeField] 
        private LayerMask _layerMask;

        private void Start() => _camera = Camera.main;

        private void Update() => CheckForHover();

        private void CheckForHover()
        {
            if (InputManager.Instance.PointerState == PointerState.pressed)
                return;

            var go = InputManager.Instance.GetPointedObject(_camera, _layerMask);

            if (go.CompareTag("Tile") && go != _currentGO)
            {
                _prevTile = _currTile;
                _currTile = go?.GetComponent<GridTile>();
                _currentGO = go;

                InvokeOnHover();
            }
        }

        private void InvokeOnHover() 
        { 
            _prevTile?.OnHoverExit();
            _currTile?.OnHoverEnter();
        }

        private void OnEnable() 
        {
            InputManager.onPressStarted += InvokeOnPressEnter;
            InputManager.onPressEnded += InvokeOnPressExit;
        }

        private void OnDisable() 
        {
            InputManager.onPressStarted -= InvokeOnPressEnter;
            InputManager.onPressEnded -= InvokeOnPressExit;
        }

        private void InvokeOnPressEnter() => _currTile?.OnPressEnter();

        private void InvokeOnPressExit() => _currTile?.OnPressExit();
    }
}
