using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Audio Library", 
                     menuName = "Bombsite/Audio Library")]
    public class AudioLibraryAsset : ScriptableObject
    {
        [field: SerializeField, Space]
        public List<Track> Tracks { get; private set; }
        
        [System.Serializable]
        public class Track 
        {
            [field: SerializeField]
            public AudioType Type { get; private set; }

            [field: SerializeField]
            public AudioClip Clip { get; private set; }

            [field: SerializeField]
            public AudioMixerGroup MixerGroup { get; private set; }

            [field: SerializeField, Range(0.0f, 1.0f)]
            public float Volume { get; private set; }

            [field: SerializeField, Range(0.0f, 1.0f)]
            public float SpatialBlend { get; private set; }

            [field: SerializeField, Range(0.0f, 1.0f)]
            public float PitchDifference { get; private set; }
        }
    }

    public enum AudioType
    {
        Explosion,
        Click,
        Tick,
        End
    }
}
