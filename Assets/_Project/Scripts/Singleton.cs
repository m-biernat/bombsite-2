using UnityEngine;

namespace Bombsite
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() 
        {
            if (Instance != null)
            {
                Debug.LogWarning($"Instance of {this.GetType().Name} already exists. "
                                 + $"Destroying object with ID: {this.GetInstanceID()}.", this);
                Destroy(gameObject);
            }
            else
                Instance = this as T;
        }
    }
}
