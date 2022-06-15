using UnityEngine.EventSystems;

namespace Bombsite
{
    public class PointerEventTrigger : EventTrigger
    {
        private static bool _isDown = false;

        private bool _isHovering = false;

        public override void OnPointerEnter(PointerEventData data)
        {
            if (_isDown)
                return;
            
            CursorManager.Instance.SetCursorSelect();
            _isHovering = true;

            base.OnPointerEnter(data);
        }

        public override void OnPointerExit(PointerEventData data)
        {
            if (_isDown)
                return;

            CursorManager.Instance.SetCursorDefault();
            _isHovering = false;

            base.OnPointerExit(data);
        }

        public override void OnPointerDown(PointerEventData data)
        {
            _isDown = true;
            CursorManager.Instance.SetCursorSelectPressed();

            AudioManager.Instance.PlayEffect(AudioType.Click);
            
            base.OnPointerDown(data);
        } 

        public override void OnPointerUp(PointerEventData data)
        {
            _isDown = false;
            if (_isHovering)
                CursorManager.Instance.SetCursorSelect();
            else
                CursorManager.Instance.SetCursorDefault();

            base.OnPointerUp(data);
        }
    }
}
