using UnityEngine;

namespace Bombsite
{
    public class CursorController : Singleton<CursorController>
    {
        [SerializeField] 
        private Texture2D defaultCursor, selectCursor, pressedSelectCursor;

        private new void Awake() => SetCursorDefault();

        public void SetCursorDefault() => ChangeCursor(defaultCursor, Vector2.zero);

        public void SetCursorSelect()
        {
            if (InputManager.Instance.PointerState == PointerState.released)
                ChangeCursor(selectCursor, Vector2.zero);
            else
                ChangeCursor(pressedSelectCursor, Vector2.zero);
        }

        private void ChangeCursor(Texture2D cursorType, Vector2 hotSpot) 
        {
            Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto);
        }
    }
}
