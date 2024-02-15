using UnityEngine;

namespace Bombsite
{
    public class MobileSetup : MonoBehaviour
    {
        [SerializeField] bool _halfResEnabled = true;

        [SerializeField] int _targetFramerate = 30;

        public void Init()
        {
            if (!Application.isMobilePlatform)
                return;

            if (_halfResEnabled)
                SetHalfResolution();

            SetFramerate();
            SetScreenDimmingOff();

            SetBackButtonAction();
        }

        void SetHalfResolution()
        {
            var resolution = Screen.currentResolution;

            Screen.SetResolution(resolution.width / 2,
                                 resolution.height / 2,
                                 Screen.fullScreen);
        }

        void SetFramerate() => Application.targetFrameRate = _targetFramerate;

        void SetScreenDimmingOff() => Screen.sleepTimeout = SleepTimeout.NeverSleep;

        void SetBackButtonAction() => Input.backButtonLeavesApp = true;
    }
}
