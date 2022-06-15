using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Bombsite
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        [SerializeField]
        private Image _cover;

        public static event Action SceneLoaded;

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            => FadeOut(OnSceneLoaded);

        private void FadeOut(Action onComplete) 
        { 
            _cover.rectTransform.localPosition = new Vector3(0, -300, 0);
            _cover.rectTransform.DOScaleY(0, .3f).OnComplete(onComplete.Invoke);
        }

        protected virtual void OnSceneLoaded() => SceneLoaded?.Invoke();

        public void LoadMenu()
            => FadeIn(
                () => SceneManager.LoadScene(0, LoadSceneMode.Single)
            );

        public void LoadEnding()
            => FadeIn(
                () => SceneManager.LoadScene(1, LoadSceneMode.Single)
            );

        private void FadeIn(Action onComplete) 
        { 
            _cover.rectTransform.localPosition = new Vector3(0, 300, 0);
            _cover.rectTransform.DOScaleY(1, .3f).OnComplete(onComplete.Invoke);
        }

        public void LoadLevel(LevelAsset level)
            => FadeIn(
                () => SceneManager.LoadScene(level.ScenePath, LoadSceneMode.Single)
            );

        public void QuitGame()
            => FadeIn(Application.Quit);
    }
}
