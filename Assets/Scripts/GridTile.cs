using UnityEngine;

namespace Bombsite
{
    public class GridTile : MonoBehaviour
    {
        public void OnHoverEnter() => Debug.Log("On Hover Enter");

        public void OnHoverExit() => Debug.Log("On Hover Exit");

        public void OnPressEnter() => Debug.Log("On Press Enter");

        public void OnPressExit() => Debug.Log("On Press Exit");
    }
}
