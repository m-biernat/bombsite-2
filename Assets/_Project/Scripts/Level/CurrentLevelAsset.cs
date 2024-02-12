using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Current Level", 
                     menuName = "Bombsite/Current Level")]
    public class CurrentLevelAsset : ScriptableObject
    {
        [field: SerializeField]
        private LevelManagerAsset _levelManager;

        public LevelManagerAsset.LevelInfo Info 
        { get; private set; }
        
        public LevelAsset Init() 
        {
            var path = SceneManager.GetActiveScene().path;

            Info = _levelManager.GetLevelInfo(path);

            return Info.Asset;
        }
    }
}
