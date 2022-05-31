using System.Collections.Generic;
using UnityEngine;

namespace Bombsite.UI
{
    public class BombSelector : MonoBehaviour
    {
        [SerializeField]
        private BombManagerAsset _bombManager;

        [SerializeField]
        private GameObject _buttonPrefab;

        private Dictionary<BombContainer, SelectBombButton> _buttons;

        private SelectBombButton _selected;

        private void Start()
        {
            _buttons = new Dictionary<BombContainer, SelectBombButton>();

            var bombContainers = _bombManager.AvailableBombs;

            foreach (var container in bombContainers)
            {
                var go = Instantiate(_buttonPrefab.gameObject, transform);
                var button = go?.GetComponent<SelectBombButton>();
               
                if (button) 
                {
                    _buttons.Add(container, button);
                    button.Initialize(this, container);
                }
            }

            UpdateIndicator();
        }

        public void Select(BombContainer container)
        {
            _bombManager.SelectContainer(container);
            UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            var current = _bombManager.CurrentContainer;

            if (current is null)
                return;

            var button = _buttons[current];

            if (button == _selected)
                return;

            _selected?.SetIndicator(false);
            _selected = button;
            _selected.SetIndicator(true);
        }

        private void OnEnable()
            => BombManagerAsset.ValueChanged += OnValueChanged;

        private void OnDisable()
            => BombManagerAsset.ValueChanged -= OnValueChanged;

        private void OnValueChanged() 
        {
            _selected.UpdateText();
            UpdateIndicator();
        }
    }
}
