using System.Collections.Generic;
using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Level Manager", 
                     menuName = "Bombsite/Level Manager")]
    public class LevelManagerAsset : ScriptableObject
    {
        [SerializeField]
        private List<LevelGroupAsset> _levelGroups;

        public Dictionary<string, LevelInfo> Levels { get; private set; } 

        public readonly struct LevelInfo
        {
            public LevelInfo(int group, int index, LevelAsset asset)
            {
                Group = group;
                Index = index;
                Asset = asset;
            }

            public int Group { get; }

            public int Index { get; }

            public LevelAsset Asset { get; }
        } 

        public void Initialize()
        {
            Levels = new Dictionary<string, LevelInfo>();

            for (int i = 0; i < _levelGroups.Count; i++)
                for (int j = 0; j < _levelGroups[i].Levels.Count; j++) 
                {
                    var level = _levelGroups[i].Levels[j];
                    var levelInfo = new LevelInfo(i, j, level);
                    Levels.Add(level.ScenePath, levelInfo);
                }
        }
    }
}
