namespace LoadingScene.Screens
{
    using GDK.UIManager.Scripts;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class SettingScreenView : BaseView
    {
        [field: SerializeField]
        public Button BtnClose { get; private set; }
    }

    public class SettingPopup : BasePopup<SettingScreenView, object>
    {
        public override string ID => nameof(SettingScreenView);

        protected override bool IsOverlay => false;

        public SettingPopup(DiContainer container, UIManager uiManager, ILogger logger) : base(container, uiManager, logger)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            this.View.BtnClose.onClick.AddListener(this.OnClickBtnClose);
        }

        private void OnClickBtnClose()
        {
            this.Close();
        }
    }
}