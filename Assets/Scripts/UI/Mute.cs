using UnityEngine;
using UnityEngine.UI;

namespace Bombsite.UI
{
    public class Mute : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        [SerializeField]
        private Sprite _soundOn, _soundOff;

        // This will be changed
        private bool _muted = false;

        public void Toggle() 
        {
            _muted = !_muted;
            
            if (_muted)
                _image.sprite = _soundOff;
            else
                _image.sprite = _soundOn; 
        }
    }
}
