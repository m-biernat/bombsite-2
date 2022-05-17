using System;
using System.Collections;
using UnityEngine;

namespace Bombsite
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private LevelAsset _level;

        [SerializeField]
        private BombManagerAsset _bombManager;

        [SerializeField]
        private DestructibleManagerAsset _destructibleManager;

        public static event Action CountdownFinished;

        public static event Action LevelCompleted;

        public static event Action LevelFailed;

        private IEnumerator _countdown;

        private void Awake()
        {
            if (!_level)
            {
                Debug.LogError("Level Asset reference is missing!", this);
                return;
            }

            _bombManager?.Init(_level);
            _destructibleManager?.Init();
        }
        
        private void Start()
        {
            _countdown = Countdown();
            StartCoroutine(_countdown);
        }

        public IEnumerator Countdown()
        {
            yield return new WaitForSecondsRealtime(.5f);

            for (int i = 0; i < _level?.TimeLimit; i++)
            {
                yield return new WaitForSecondsRealtime(1f);
                // Update clock
            }
            _countdown = null;
            
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
            // GameManager.GamePaused += OnGamePaused
            // GameManager.GameResumed += OnGameResumed
            BombController.AllBombsPlanted += OnAllBombsPlanted;
            BombController.AllBombsDetonated += OnAllBombsDetonated;
        }

        private void OnDisable()
        {
            // GameManager.GamePaused -= OnGamePaused
            // GameManager.GameResumed -= OnGameResumed
            BombController.AllBombsPlanted -= OnAllBombsPlanted;
            BombController.AllBombsDetonated -= OnAllBombsDetonated;
        }

        private void OnGamePaused() {}

        private void OnGameResumed() {}

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
