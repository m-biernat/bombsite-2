using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Bombsite
{
    public class BombLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void Awake()
            => _text.alpha = 0.0f;

        public void Init(int value)
        {
            _text.text = value.ToString();
            _text.DOFade(1.0f, 0.5f);
        }
    }
}
