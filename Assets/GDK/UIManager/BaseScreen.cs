namespace GDK.UIManager
{
    using System.Reflection;
    using UnityEngine;
    using Zenject;

    public abstract class BaseScreen : MonoBehaviour
    {
        private string id;

        public string ID => this.id ??=
            (this.GetType().GetCustomAttribute(typeof(ScreenInfoAttribute)) as ScreenInfoAttribute)?.ID ??
            this.GetType().Name;

        public object Data { get; internal set; }

        public bool IsVisible => this.transform.parent != this.UIManager.Temp;

        internal virtual void Init()
        {
            this.gameObject.SetActive(false);
            this.gameObject.name = this.ID;
            this.OnInit();
        }

        internal virtual void Dispose()
        {
            DestroyImmediate(this.gameObject);
            this.OnDispose();
        }

        internal virtual void Show()
        {
            this.gameObject.SetActive(true);
            this.OnShow();
        }

        internal virtual void Hide()
        {
            this.gameObject.SetActive(false);
            this.OnHide();
        }

        protected virtual void OnInit()
        {
            this.Logger.Log($"Screen <color=green>{this.ID}</color> initialized");
        }

        protected virtual void OnDispose()
        {
            this.Logger.Log($"Screen <color=green>{this.ID}</color> disposed");
        }

        protected virtual void OnShow()
        {
            this.Logger.Log($"Screen <color=green>{this.ID}</color> shown");
        }

        protected virtual void OnHide()
        {
            this.Logger.Log($"Screen <color=green>{this.ID}</color> hidden");
        }

        public void Open()
        {
            this.UIManager.OpenScreen(this.GetType());
        }

        public void Close()
        {
            this.UIManager.CloseScreen(this.GetType());
        }

        #region Inject

        protected DiContainer Container { get; private set; }
        protected UIManager   UIManager { get; private set; }
        protected ILogger     Logger    { get; private set; }

        [Inject]
        protected virtual void Inject(DiContainer container, UIManager uiManager, ILogger logger)
        {
            this.Container = container;
            this.UIManager = uiManager;
            this.Logger    = logger;
        }

        #endregion
    }
}