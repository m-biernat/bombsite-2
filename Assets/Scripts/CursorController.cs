using UnityEngine;

namespace Bombsite
{
    public class CursorController : Singleton<CursorController>
    {
        [SerializeField]
        private CursorAsset _cursor;

        protected override void Awake() 
        {
            base.Awake();
            SetCursorDefault();
        }

        public void SetCursorDefault() 
            => ChangeCursor(_cursor.Default, Vector2.zero);

        public void SetCursorSelect()
        {
            if (InputManager.Instance.PointerState == PointerState.released)
                ChangeCursor(_cursor.Select, Vector2.zero);
            else
                ChangeCursor(_cursor.SelectPressed, Vector2.zero);
        }

        private void ChangeCursor(Texture2D cursorType, Vector2 hotSpot) 
            => Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto);
    }
}
