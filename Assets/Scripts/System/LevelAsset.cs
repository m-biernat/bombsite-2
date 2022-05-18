using UnityEngine;
using System.Collections.Generic;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "New Level", 
                     menuName = "Bombsite/Level")]
    public class LevelAsset : ScriptableObject
    {
        [field: SerializeField]
        public int TimeLimit { get; private set; }

        [field: SerializeField]
        public List<BombContainer> AvailableBombs 
        { get; private set; }
    }
}
