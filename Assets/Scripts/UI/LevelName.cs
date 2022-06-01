using UnityEngine;
using TMPro;

namespace Bombsite.UI
{
    public class LevelName : MonoBehaviour
    {
        [SerializeField]
        private CurrentLevelAsset _currentLevel;

        [Space, SerializeField]
        private TMP_Text _text;

        private void Start()
        {
            var info = _currentLevel.Info;
            string name = $"L. {info.Group + 1}.{(info.Index):01}";
            _text.text = name;
        }
    }
}
