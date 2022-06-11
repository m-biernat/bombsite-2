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

        private Tween _fade;

        public static event Action<Tile> Pressing;

        public static event Action Pressed;

        public void OnPressing() 
            => Pressing?.Invoke(this);

        public void OnPressed()
            => Pressed?.Invoke();

        public void Hide() 
            => _fade = _image?.FadeOut(.5f);

        private void OnDestroy() => _fade?.Kill();
    }
}
