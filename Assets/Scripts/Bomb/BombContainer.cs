using UnityEngine;

namespace Bombsite
{
    [System.Serializable]
    public class BombContainer
    {
        [SerializeField]
        private BombAsset _bomb;
        
        public BombAsset Bomb { get => _bomb; }

        [SerializeField]
        private int _count;

        public int Count { get => _count; }

        public BombContainer(BombContainer item)
        {
            _bomb = item.Bomb;
            _count = item.Count;
        }

        public void DecreaseCount()
        {
            if (Count > 0)
                _count--;
        }

        public bool IsEmpty()
        {
            if (Count <= 0)
                return true;
            else
                return false;
        }
    }
}
