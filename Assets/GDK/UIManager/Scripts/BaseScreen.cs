namespace GDK.UIManager.Scripts
{
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Zenject;

    public abstract class BaseScreen
    {
        public abstract string ID { get; }

        public BaseView View { get; internal set; }

        public object Data { get; internal set; }

        public bool IsVisible => this.View.transform.parent != this.UIManager.Temp;

        internal virtual void Init()
        {
            var prefab = Addressables.LoadAssetAsync<GameObject>(this.ID).WaitForCompletion();

            this.View = this.Container.InstantiatePrefab(prefab, this.UIManager.Temp).GetComponent<BaseView>();

            this.OnInit();
        }

        internal virtual void Dispose()
        {
            Object.DestroyImmediate(this.View.gameObject);
            this.OnDispose();
        }

        internal virtual void Show()
        {
            this.OnShow();
        }

        internal virtual void Hide()
        {
            this.OnHide();
        }

        protected virtual void OnInit()
        {
            this.Logger.Log($"Screen {this.ID} initialized");
        }

        protected virtual void OnDispose()
        {
            this.Logger.Log($"Screen {this.ID} disposed");
        }

        protected virtual void OnShow()
        {
            this.Logger.Log($"Screen {this.ID} shown");
        }

        protected virtual void OnHide()
        {
            this.Logger.Log($"Screen {this.ID} hidden");
        }

        #region Inject

        protected DiContainer Container { get; }
        protected UIManager   UIManager { get; }
        protected ILogger     Logger    { get; }

        protected BaseScreen(DiContainer container, UIManager uiManager, ILogger logger)
        {
            this.Container = container;
            this.UIManager = uiManager;
            this.Logger    = logger;
        }

        #endregion
    }
}