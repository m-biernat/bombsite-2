using System;
using System.Collections;
using UnityEngine;

namespace Bombsite
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
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

        private WaitForSecondsRealtime _extraDelay, _timeDelay;

        private void Awake()
        {
            if (!_level)
            {
                Debug.LogError("Level Asset reference is missing!", this);
                return;
            }

            _bombManager?.Init(_level);
            _destructibleManager?.Init();
            
            _time.Initialize(_level.TimeLimit);
            _extraDelay = new WaitForSecondsRealtime(.5f);
            _timeDelay = new WaitForSecondsRealtime(1);
        }
        
        private void Start()
        {
            _countdown = Countdown();
            StartCoroutine(_countdown);
        }

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

        protected virtual void OnLevelCompleted()
            => LevelCompleted?.Invoke();

        protected virtual void OnLevelFailed()
            => LevelFailed?.Invoke();

        private void OnEnable()
        {
            BombController.AllBombsPlanted += OnAllBombsPlanted;
            BombController.AllBombsDetonated += OnAllBombsDetonated;
        }

        private void OnDisable()
        {
            BombController.AllBombsPlanted -= OnAllBombsPlanted;
            BombController.AllBombsDetonated -= OnAllBombsDetonated;
        }

        private void OnAllBombsPlanted()
        {
            if (_countdown is null)
                return;
            
            StopCoroutine(_countdown);

            OnCountdownFinished();
        }

        private void OnNoBombsDetonated()
            => OnLevelFailed();

        private void OnAllBombsDetonated()
        {
            if (_bombManager.AllBombsUsed())
                OnLevelCompleted();
            else
                OnLevelFailed();
        }
    }
}
