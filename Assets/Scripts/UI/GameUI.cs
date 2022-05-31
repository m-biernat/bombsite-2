using UnityEngine;
using DG.Tweening;

namespace Bombsite.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _onCountdownFinished;

        private void OnEnable()
        {
            GameController.CountdownFinished += OnCountdownFinished;
        }

        private void OnDisable()
        {
            GameController.CountdownFinished -= OnCountdownFinished;
        }

        private void OnCountdownFinished() 
            => Hide(_onCountdownFinished);

        public void Hide(CanvasGroup group) 
        {
            group.blocksRaycasts = false;
            group.DOFade(0f, .5f)
                        .OnComplete(
                            () => group.gameObject.SetActive(false));
        }
    }
}
