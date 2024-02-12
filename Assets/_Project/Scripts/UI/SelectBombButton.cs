using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bombsite.UI
{
    public class SelectBombButton : MonoBehaviour
    {
        private BombSelector _selector;

        private BombContainer _container;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private GameObject _indicator;

        public void Initialize(BombSelector selector,
                               BombContainer container)
        {
            _selector = selector;
            _container = container;
            
            _icon.sprite = _container.Bomb.Icon;
            UpdateText();
        }

        public void UpdateText() 
            => _text.text = _container.Count.ToString();

        public void SetIndicator(bool active) 
            => _indicator.SetActive(active);

        public void SelectBomb()
            => _selector.Select(_container);
    }
}
