using UnityEngine;

namespace Bombsite
{
    public class Tile : MonoBehaviour, IPointerAction
    {
        public void OnHovering() => Debug.Log("On Hover Enter");

        public void OnHovered() => Debug.Log("On Hover Exit");

        public void OnPressing() => Debug.Log("On Press Enter");

        public void OnPressed() => Debug.Log("On Press Exit");

        public void Hide() {}
    }
}
