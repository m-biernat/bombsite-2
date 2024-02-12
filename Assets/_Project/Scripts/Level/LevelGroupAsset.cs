using System.Collections.Generic;
using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "New Level Group", 
                     menuName = "Bombsite/Level Group")]
    public class LevelGroupAsset : ScriptableObject
    {
        [field: SerializeField]
        public List<LevelAsset> Levels { get; private set; }
    }
}
