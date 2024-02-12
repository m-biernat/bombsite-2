using System;
using UnityEngine;

namespace Bombsite
{
    public abstract class VariableAsset<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        [NonSerialized]
        protected T _value;

        public T Value 
        { 
            get { return _value; }
            set 
            {
                _value = value;
                OnValueChanged();
            }
        }

        public event Action ValueChanged;

        [field: SerializeField]
        public T InitialValue { get; set; }

        public void OnAfterDeserialize() 
            => _value = InitialValue;

        public void OnBeforeSerialize() { }

        protected virtual void OnValueChanged() 
            => ValueChanged?.Invoke();

        public void Init(T value) 
            => _value = value;
    }
}
