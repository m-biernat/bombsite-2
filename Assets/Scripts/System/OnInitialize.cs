using UnityEngine;
using UnityEngine.Events;

namespace Bombsite
{
    public class OnInitialize : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _invokeOnInitialize;

        private void Awake()
        {
            _invokeOnInitialize?.Invoke();
            Destroy(gameObject);
        }
    }
}
