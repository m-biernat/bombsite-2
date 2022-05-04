using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "New Bomb", 
                     menuName = "Bombsite/Bomb")]
    public class BombAsset : ScriptableObject
    {
        [field: SerializeField]
        public Texture2D Icon { get; private set; }

        [field: SerializeField]
        public GameObject Prefab { get; private set; }       
    }
}