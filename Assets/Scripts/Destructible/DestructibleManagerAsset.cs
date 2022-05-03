using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Destructible Manager", 
                     menuName = "Bombsite/Destructible Manager")]
    public class DestructibleManagerAsset : ScriptableObject
    {
        //private List<IDestructible> _destructibles;   This will be needed
        //                                              for marking undamaged

        public int TotalDestructibles { get; private set; }

        public int DestructedCount { get; private set; }

        public static event Action ValueChanged;

        public void Init() 
        {
            var destructibles = 
                GameObject.FindGameObjectsWithTag("Destructible");
                
            TotalDestructibles = destructibles.Length;
            DestructedCount = 0;
        }

        public void OnDestructed()
        {
            DestructedCount++;
            OnValueChanged();
        }

        protected virtual void OnValueChanged() 
            => ValueChanged?.Invoke();

        public bool AllDestructed()
            => DestructedCount == TotalDestructibles;
    }
}
