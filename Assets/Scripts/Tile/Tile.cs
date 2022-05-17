using UnityEngine;

namespace Bombsite
{
    public class Tile : MonoBehaviour, IPointerAction
    {
        public bool Hidden { get; private set; } = false;

        private bool _hovering = false;

        public void OnHovering() 
        {
            ToggleHovering();
            CursorController.Instance?.SetCursorSelect();
        }

        private void ToggleHovering()
            => _hovering = !_hovering;

        public void OnHovered() 
        {
            ToggleHovering();
            CursorController.Instance?.SetCursorDefault();
        } 

        public void OnPressing() 
        {
            CursorController.Instance?.SetCursorSelect();
        }

        public void OnPressed() 
        {
            if (_hovering)
                CursorController.Instance?.SetCursorSelect();
            else
                CursorController.Instance?.SetCursorDefault();
        }

        public void Hide() 
        {
            Hidden = true;
        }
    }
}
