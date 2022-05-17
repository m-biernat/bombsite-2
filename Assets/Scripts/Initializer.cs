using UnityEngine;

namespace Bombsite
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute() 
            => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems"))); 
    }
}
