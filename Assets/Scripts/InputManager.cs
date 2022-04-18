using UnityEngine;

namespace Bombsite
{
    public class InputManager : Singleton<InputManager>
    {
        private InputControls _controls;

        public PointerState PointerState { get; private set; }

        public delegate void OnPressStarted();
        public static event OnPressStarted onPressStarted;

        public delegate void OnPressEnded();
        public static event OnPressEnded onPressEnded;

        protected override void Awake() 
        {
            base.Awake();
            _controls = new InputControls();
            PointerState = PointerState.released;
        }

        private void OnEnable() => _controls?.Enable();

        private void OnDisable() => _controls?.Disable();

        private void Start() 
        {
            if (_controls is null)
                Debug.LogError($"{nameof(InputControls)} is null!", this);
            else
                SubscribeToPointerPressEvents();  
        }

        private void SubscribeToPointerPressEvents()
        {
            _controls.Pointer.Press.started += _ => PressStarted();
            _controls.Pointer.Press.performed += _ => PressEnded();
        }

        private void PressStarted() 
        {
            PointerState = PointerState.pressed;
            onPressStarted?.Invoke();
        }

        private void PressEnded() 
        {
            PointerState = PointerState.released;
            onPressEnded?.Invoke();
        }

        public GameObject GetPointedObject(Camera camera, int layerMask)
        {
            var position = _controls.Pointer.Position.ReadValue<Vector2>();
            Ray ray = camera.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                return hit.collider.gameObject;
            else
                return null;
        }
    }

    public enum PointerState 
    {
        released,
        pressed
    }
}
