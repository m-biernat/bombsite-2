using UnityEngine;

namespace Bombsite
{
    [CreateAssetMenu(fileName = "Cursor", 
                     menuName = "Bombsite/Cursor")]
    public class CursorAsset : ScriptableObject
    {
        [field: SerializeField]
        public Texture2D Default { get; private set; }
  
        [field: SerializeField]
        public Texture2D Select { get; private set; }

        [field: SerializeField]
        public Texture2D SelectPressed { get; private set; }
    }
}
