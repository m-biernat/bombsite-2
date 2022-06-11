using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Bombsite
{
    public static class Extensions
    {
        public static Tween Fade(this CanvasGroup group, float target, float time, bool active)
        {
            group.blocksRaycasts = active;
            return group.DOFade(target, time)
                        .OnComplete(() => group.gameObject.SetActive(active));
        }

        public static Tween Fade(this Image image, float target, float time, bool active)
        {
            image.raycastTarget = active;
            return image.DOFade(target, time)
                        .OnComplete(() => image.gameObject.SetActive(active));
        }
    }
}
