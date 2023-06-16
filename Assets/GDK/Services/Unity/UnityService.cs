namespace GDK.Services.Unity
{
    using UnityEngine;
    using Zenject;

    public class UnityService : MonoBehaviour
    {
        private void OnApplicationFocus(bool hasFocus)
        {
            this.SignalBus.Fire(new ApplicationFocusSignal { HasFocus = hasFocus });
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            this.SignalBus.Fire(new ApplicationPauseSignal { PauseStatus = pauseStatus });
        }

        private void OnApplicationQuit()
        {
            this.SignalBus.Fire<ApplicationQuitSignal>();
        }

        #region Inject

        protected SignalBus SignalBus { get; set; }

        [Inject]
        private void Inject(SignalBus signalBus)
        {
            this.SignalBus = signalBus;
        }

        #endregion
    }
}