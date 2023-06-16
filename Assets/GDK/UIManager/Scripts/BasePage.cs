namespace GDK.UIManager.Scripts
{
    public abstract class BasePage : BaseScreen
    {
        internal override void Show()
        {
            this.transform.SetParent(this.UIManager.Page);
            base.Show();
        }

        internal override void Hide()
        {
            this.transform.SetParent(this.UIManager.Temp);

            foreach (BasePopup popup in this.UIManager.Page) popup.Hide();

            base.Hide();
        }
    }

    public abstract class BasePage<TData> : BasePage
        where TData : class
    {
        public new TData Data => base.Data as TData;
    }
}