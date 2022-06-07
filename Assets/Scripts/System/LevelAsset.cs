using UnityEngine;
using System.Collections.Generic;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "New Level", 
                     menuName = "Bombsite/Level")]
    public class LevelAsset : ScriptableObject
    {   
        [SerializeField, HideInInspector]
        private string _scenePath;

        public string ScenePath { get => _scenePath; }

        [field: SerializeField]
        public int TimeLimit { get; private set; }

        [field: SerializeField]
        public List<BombContainer> AvailableBombs 
        { get; private set; }

        public void Initialize(string scenePath)
            => _scenePath = scenePath;
    }
}
