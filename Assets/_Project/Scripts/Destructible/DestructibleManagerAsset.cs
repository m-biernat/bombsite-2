using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Destructible Manager", 
                     menuName = "Bombsite/Destructible Manager")]
    public class DestructibleManagerAsset : ScriptableObject
    {
        private List<IDestructible> _destructibles;

        public int TotalDestructibles { get; private set; }

        public int DestructedCount { get; private set; }

        public static event Action ValueChanged;

        public void Init() 
        {
            _destructibles = new List<IDestructible>();
            
            var gameObjects = 
                GameObject.FindGameObjectsWithTag("Destructible");

            foreach (var go in gameObjects)
            {
                var destructible = go.GetComponent<IDestructible>();
                
                if (destructible != null)
                    _destructibles.Add(destructible);
            }
                
            TotalDestructibles = _destructibles.Count;
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

        public void MarkAllUndamaged()
        {
            foreach (var destructible in _destructibles)
                if(!destructible.Destructed)
                    destructible.MarkUndamaged();
        }
    }
}
