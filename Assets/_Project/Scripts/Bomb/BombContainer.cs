using UnityEngine;

namespace Bombsite
{
    [System.Serializable]
    public class BombContainer
    {
        [field: SerializeField]
        public BombAsset Bomb { get; private set; }

        [field: SerializeField]
        public int Count { get; private set; }

        public BombContainer(BombContainer item)
        {
            Bomb = item.Bomb;
            Count = item.Count;
        }

        public void DecreaseCount()
        {
            if (Count > 0)
                Count--;
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
