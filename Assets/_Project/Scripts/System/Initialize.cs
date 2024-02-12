using UnityEngine;
using UnityEngine.Events;

namespace Bombsite
{
    public class Initialize : MonoBehaviour
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
