using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "New Bomb", 
                     menuName = "Bombsite/Bomb")]
    public class BombAsset : ScriptableObject
    {
        [SerializeField]
        private Texture2D _icon;

        public Texture2D Icon { get => _icon; }

        [SerializeField]
        private GameObject _prefab;

        public GameObject Prefab { get => _prefab; }       
    }
}