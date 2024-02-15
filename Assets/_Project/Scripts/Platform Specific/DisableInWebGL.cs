using UnityEngine;

namespace Bombsite
{
    public class DisableInWebGL : MonoBehaviour
    {
    #if UNITY_WEBGL
        private void Awake() => gameObject.SetActive(false);
    #endif
    }
}
