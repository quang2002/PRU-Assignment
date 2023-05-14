namespace GDK.UIManager.Scripts
{
    using UnityEngine;
    using Zenject;

    public abstract class BasePage : BaseScreen
    {
        protected BasePage(DiContainer container, UIManager uiManager, ILogger logger) : base(container, uiManager, logger)
        {
        }

        internal override void Show()
        {
            this.View.transform.SetParent(this.UIManager.Page);
            base.Show();
        }

        internal override void Hide()
        {
            this.View.transform.SetParent(this.UIManager.Temp);

            foreach (BasePopup popup in this.UIManager.Page) popup.Hide();

            base.Hide();
        }
    }

    public abstract class BasePage<TView, TModel> : BasePage
        where TView : BaseView
        where TModel : class
    {
        protected BasePage(DiContainer container, UIManager uiManager, ILogger logger) : base(container, uiManager, logger)
        {
        }

        public new TView  View => base.View as TView;
        public new TModel Data => base.Data as TModel;
    }
}