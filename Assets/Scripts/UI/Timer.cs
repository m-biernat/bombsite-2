using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Bombsite.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Color _zeroColor;
        
        [SerializeField]
        private IntVariable _time;

        [SerializeField]
        private RectTransform _fill;

        private Vector2 _fillSize;

        private float _fillStep;

        private void Start() => Init();

        private void Init() 
        {
            SetText();
            _fillSize = _fill.sizeDelta;
            _fillStep = _fillSize.x / _time.Value;
        }

        private void SetText()
            => _text.text = _time.Value.ToString();

        private void OnEnable() 
            => _time.ValueChanged += OnValueChanged;

        private void OnDisable() 
            => _time.ValueChanged -= OnValueChanged;

        private void OnValueChanged() {
            SetText();
            
            if (_time.Value <= 0) 
            {
                _text.color = _zeroColor;
                
                DOTween.Sequence()
                    .Append(_text.transform.DOScale(1.5f, .1f))
                    .Append(_text.transform.DOScale(1.0f, .2f).SetEase(Ease.OutBounce));
            }
                
            _fillSize.x -= _fillStep;
            _fill.DOSizeDelta(_fillSize, .5f).SetEase(Ease.OutBounce);
        }
    }
}
