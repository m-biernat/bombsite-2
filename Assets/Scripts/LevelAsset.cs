using UnityEngine;
using System.Collections.Generic;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "New Level", 
                     menuName = "Bombsite/Level")]
    public class LevelAsset : ScriptableObject
    {
        [SerializeField]
        private float _timeLimit;

        public float TimeLimit { get => _timeLimit; }

        [SerializeField]
        private List<BombContainer> _availableBombs;

        public List<BombContainer> AvailableBombs 
        { get => _availableBombs; }
    }
}
