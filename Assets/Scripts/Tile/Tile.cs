using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Bombsite
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public static event Action<Tile> Pressing;

        public static event Action Pressed;

        public void OnPressing() 
            => Pressing?.Invoke(this);

        public void OnPressed()
            => Pressed?.Invoke();

        public void Hide()
        {
            _image.raycastTarget = false;
            _image.DOFade(0f, .5f)
                  .OnComplete(
                      () => gameObject.SetActive(false));
        }
    }
}
