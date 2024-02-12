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
            string name = $"L. {info.Group}.{(info.Index + 1):00}";
            _text.text = name;
        }
    }
}
