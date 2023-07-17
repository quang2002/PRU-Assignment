namespace GDK.UIManager
{
    public abstract class BasePopup : BaseScreen
    {
        protected virtual bool IsOverlay => false;

        internal override void Show()
        {
            this.transform.SetParent(this.IsOverlay ? this.UIManager.Overlay : this.UIManager.Page);
            base.Show();
        }

        internal override void Hide()
        {
            this.transform.SetParent(this.UIManager.Temp);
            base.Hide();
        }
    }

    public abstract class BasePopup<TData> : BasePopup
        where TData : class
    {
        public new TData Data => base.Data as TData;
    }
}