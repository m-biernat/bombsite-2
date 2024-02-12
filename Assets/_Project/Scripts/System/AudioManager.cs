using UnityEngine;

namespace Bombsite
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField]
        private AudioLibraryAsset _audioLibrary;

        private GameObject _gameObject;

        private AudioSource _audioSource;

        private float _volume;

        protected override void Awake()
        {
            base.Awake();
            
            _volume = AudioListener.volume;
        }

        public void PlayEffect(AudioType effect) 
            => PlayEffect(effect, Vector3.zero);

        public void PlayEffect(AudioType effect, Vector3 position)
        {
            if (!_gameObject)
            {
                _gameObject = new GameObject("Sound Effect");
                
                _audioSource = _gameObject.AddComponent<AudioSource>();
            }

            _gameObject.transform.position = position;

            var track = _audioLibrary.Tracks.Find(
                (track) => track.Type == effect
            );

            _audioSource.outputAudioMixerGroup = track.MixerGroup;
            _audioSource.spatialBlend = track.SpatialBlend;
            
            var pitch = _audioSource.pitch;
            _audioSource.pitch = Random.Range(pitch
                - track.PitchDifference, pitch + track.PitchDifference);

            _audioSource.PlayOneShot(track.Clip, track.Volume);
        }

        public void Mute()
        {
            AudioListener.pause = true;
            AudioListener.volume = 0.0f;
        }

        public void Unmute()
        {
            AudioListener.pause = false;
            AudioListener.volume = _volume;
        }
    }
}
