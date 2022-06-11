using System.Collections.Generic;
using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Level Manager", 
                     menuName = "Bombsite/Level Manager")]
    public class LevelManagerAsset : ScriptableObject
    {
        [field: SerializeField]
        public List<LevelGroupAsset> LevelGroups { get; private set; }

        private List<string> _levelPaths;

        private List<LevelInfo> _levelInfos;

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

        public void Init()
        {
            _levelPaths = new List<string>();
            _levelInfos = new List<LevelInfo>();

            for (int i = 0; i < LevelGroups.Count; i++)
                for (int j = 0; j < LevelGroups[i].Levels.Count; j++) 
                {
                    var level = LevelGroups[i].Levels[j];
                    var levelInfo = new LevelInfo(i, j, level);
                    _levelPaths.Add(level.ScenePath);
                    _levelInfos.Add(levelInfo);
                }
        }

        public LevelInfo GetLevelInfo(string path)
            => _levelInfos[_levelPaths.IndexOf(path)];

        public LevelInfo? GetLevelInfo(int index)
        {
            if (index >= 0 && index < _levelInfos.Count)
                return _levelInfos[index];
            else
                return null;
        }

        public int GetIndexOfLevelInfo(LevelInfo levelInfo)
            => _levelPaths.IndexOf(levelInfo.Asset.ScenePath);

        public void SwapGroups(LevelAsset level, int from, int to)
        {
            if (from > -1)
                LevelGroups[from].Levels.Remove(level);

            LevelGroups[to].Levels.Add(level);
        }
    }
}
