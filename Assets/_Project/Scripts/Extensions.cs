using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Bombsite
{
    public static class Extensions
    {
        public static Tween FadeOut(this CanvasGroup group, float time, Action onComplete = null)
        {
            group.blocksRaycasts = false;
            return group.DOFade(0, time)
                        .OnComplete(() => { 
                            onComplete?.Invoke();
                            group.gameObject.SetActive(false); 
                        });
        }

        public static Tween FadeIn(this CanvasGroup group, float time, Action onComplete = null)
        {
            group.gameObject.SetActive(true);
            group.blocksRaycasts = true;
            return group.DOFade(1, time).OnComplete(() => onComplete?.Invoke());
        }

        public static Tween FadeOut(this Image image, float time, Action onComplete = null)
        {
            image.raycastTarget = false;
            return image.DOFade(0, time)
                        .OnComplete(() => { 
                            onComplete?.Invoke();
                            image.gameObject.SetActive(false); 
                        });
        }
    }
}
