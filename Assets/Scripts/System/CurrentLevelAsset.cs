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

        public LevelAsset UpdateInfo() 
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            var path = SceneUtility.GetScenePathByBuildIndex(index);

            Info = _levelManager.Levels[path];
            
            return Info.Asset;
        }
    }
}
