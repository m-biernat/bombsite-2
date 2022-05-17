using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Bomb Manager", 
                     menuName = "Bombsite/Bomb Manager")]
    public class BombManagerAsset : ScriptableObject
    {
        public BombContainer CurrentContainer 
        { get; private set; }

        public List<BombContainer> AvailableBombs 
        { get; private set; }

        private int _initialBombCount;

        public int TotalBombCount { get; private set; }

        public static event Action ValueChanged;

        public List<IBomb> DetonableBombs
        { get; private set; }

        public void Init(LevelAsset level)
        {
            CurrentContainer = null;
            AvailableBombs = new List<BombContainer>();
            TotalBombCount = 0;

            foreach (var container in level?.AvailableBombs)
            {
                AvailableBombs.Add(new BombContainer(container));
                TotalBombCount += container.Count;
            }
            
            _initialBombCount = TotalBombCount;

            if (AvailableBombs.Count > 0)
                CurrentContainer = AvailableBombs[0];
            else
                Debug.LogError("List of available bombs is empty", this);

            OnValueChanged();
        }

        protected virtual void OnValueChanged() 
            => ValueChanged?.Invoke();

        
        public GameObject GetBombPrefab()
            => CurrentContainer.Bomb.Prefab;
        
        public void RemoveFromContainer()
        {
            if (TotalBombCount <= 0)
                return;

            CurrentContainer.DecreaseCount();

            if (CurrentContainer.IsEmpty())
                FindNewContainer();

            TotalBombCount--;
            OnValueChanged();
        }

        private void FindNewContainer()
        {
            CurrentContainer = 
                AvailableBombs.Find(item => item.Count > 0);
        }

        public void SelectContainer(BombContainer container)
        {
            if (container.Count > 0)
            {
                CurrentContainer = container;        
                OnValueChanged();
            }
        }

        public void RegisterBomb(IBomb bomb)
        {
            if (!bomb.Detonable)
                return;

            DetonableBombs.Add(bomb);
            bomb.SetID(DetonableBombs.Count);
        }

        public bool AllBombsUsed()
            => TotalBombCount == 0;

        public bool NoBombsUsed()
            => TotalBombCount == _initialBombCount;
    }
}
