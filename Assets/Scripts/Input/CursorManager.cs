using UnityEngine;

namespace Bombsite
{
    public class CursorManager : Singleton<CursorManager>
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
            => ChangeCursor(_cursor.Select, Vector2.zero);

        public void SetCursorSelectPressed()
            => ChangeCursor(_cursor.SelectPressed, Vector2.zero);

        private void ChangeCursor(Texture2D cursorType, Vector2 hotSpot) 
            => Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto);
    }
}
