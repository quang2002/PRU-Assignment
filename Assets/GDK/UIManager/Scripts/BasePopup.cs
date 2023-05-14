namespace GDK.UIManager.Scripts
{
    using UnityEngine;
    using Zenject;

    public abstract class BasePopup : BaseScreen
    {
        protected BasePopup(DiContainer container, UIManager uiManager, ILogger logger) : base(container, uiManager, logger)
        {
        }

        protected virtual bool IsOverlay => false;

        internal override void Show()
        {
            this.View.transform.SetParent(this.IsOverlay ? this.UIManager.Overlay : this.UIManager.Page);
            base.Show();
        }

        internal override void Hide()
        {
            this.View.transform.SetParent(this.UIManager.Temp);
            base.Hide();
        }
    }

    public abstract class BasePopup<TView, TModel> : BasePopup
        where TView : BaseView
        where TModel : class
    {
        protected BasePopup(DiContainer container, UIManager uiManager, ILogger logger) : base(container, uiManager, logger)
        {
        }

        public new TView  View => base.View as TView;
        public new TModel Data => base.Data as TModel;
    }
}