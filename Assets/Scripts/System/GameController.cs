using System;
using System.Collections;
using UnityEngine;

namespace Bombsite
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private CurrentLevelAsset _currentLevel;

        private LevelAsset _level;

        [Space, SerializeField]
        private BombManagerAsset _bombManager;

        [SerializeField]
        private DestructibleManagerAsset _destructibleManager;

        public static event Action CountdownFinished;

        public static event Action LevelCompleted;

        public static event Action LevelFailed;

        private IEnumerator _countdown;

        [Space, SerializeField]
        private IntVariable _time;

        private WaitForSecondsRealtime _timeDelay, _extraDelay;

        [SerializeField]
        private float _timeDelayValue = 1.0f, _extraDelayValue = .5f;

        private void Awake()
        {
            _level = _currentLevel?.Init();

            if (!_level)
            {
                Debug.LogError("Level Asset reference is missing!", this);
                return;
            }

            _bombManager?.Init(_level);
            _destructibleManager?.Init();
            
            _time.Init(_level.TimeLimit);
            _timeDelay = new WaitForSecondsRealtime(_timeDelayValue);
            _extraDelay = new WaitForSecondsRealtime(_extraDelayValue);

            _countdown = Countdown();
        }
        
        private void OnEnable()
        {
            SceneLoader.SceneLoaded += OnSceneLoaded;
            BombController.AllBombsPlanted += OnAllBombsPlanted;
            BombController.NoBombsDetonated += OnNoBombsDetonated;
            BombController.AllBombsDetonated += OnAllBombsDetonated;
        }

        private void OnDisable()
        {
            SceneLoader.SceneLoaded -= OnSceneLoaded;
            BombController.AllBombsPlanted -= OnAllBombsPlanted;
            BombController.NoBombsDetonated -= OnNoBombsDetonated;
            BombController.AllBombsDetonated -= OnAllBombsDetonated;
        }

        private void OnSceneLoaded() => StartCoroutine(_countdown);

        public IEnumerator Countdown()
        {
            yield return _extraDelay;

            var limit = _level.TimeLimit;

            for (int i = 1; i <= limit; i++)
            {
                yield return _timeDelay;
                _time.Value = limit - i;
            }
            _countdown = null;

            yield return _extraDelay;
            
            OnCountdownFinished();
        }

        protected virtual void OnCountdownFinished()
            => CountdownFinished?.Invoke();

        private void OnAllBombsPlanted()
        {
            if (_countdown is null)
                return;
            
            StopCoroutine(_countdown);

            Invoke("OnCountdownFinished", _extraDelayValue);
        }

        private void OnNoBombsDetonated()
        {
            _destructibleManager.MarkAllUndamaged();
            OnLevelFailed();
        }

        protected virtual void OnLevelFailed()
            => LevelFailed?.Invoke();

        private void OnAllBombsDetonated()
            => Invoke("DetermineResults", _level.WaitTime);

        private void DetermineResults() 
        {
            _destructibleManager.MarkAllUndamaged();

            if (_destructibleManager.AllDestructed())
                OnLevelCompleted();
            else
                OnLevelFailed();
        }

        protected virtual void OnLevelCompleted()
            => LevelCompleted?.Invoke();
    }
}
