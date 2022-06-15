using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bombsite.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private TMP_Dropdown _levelDropdown;

        [Space, SerializeField]
        private GameManagerAsset _gameManager;

        [SerializeField]
        private LevelManagerAsset _levelManager;

        private int _groupOffset;

        private LevelAsset _selectedLevel;

        [Space, SerializeField]
        private GameObject _bomb;

        private void Awake() => FillDropdown();

        private void FillDropdown()
        {
            _levelDropdown.options.Clear();

            _groupOffset = _levelManager.LevelGroups[0].Levels.Count;
            var count = _levelManager.LevelInfos.Count;

            for (int i = _groupOffset; i < count; i++)
            {
                var levelInfo = _levelManager.LevelInfos[i];
                _levelDropdown.options.Add(
                    new TMP_Dropdown.OptionData() 
                    {
                        text = $"L {levelInfo.Group}.{(levelInfo.Index + 1):00}"
                    }
                );
            }

            DropdownItemSelected(0);

            _levelDropdown.onValueChanged.AddListener(
                (value) => DropdownItemSelected(value)
            );
        }

        private void DropdownItemSelected(int index)
        {
            var levelInfo = _levelManager.LevelInfos[index + _groupOffset];
            _selectedLevel = levelInfo.Asset;
        }

        public void Play() => _gameManager.LoadLevel(_selectedLevel);
    }
}
