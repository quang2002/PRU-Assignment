namespace LoadingScene.Screens
{
    using GDK.GDKUtils.Scripts;
    using GDK.UIManager.Scripts;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class LoadingScreenView : BaseView
    {
        [field: SerializeField]
        public Button BtnNewGame { get; private set; }

        [field: SerializeField]
        public Button BtnSetting { get; private set; }

        [field: SerializeField]
        public Button BtnExit { get; private set; }
    }

    public class LoadingPage : BasePage<LoadingScreenView, object>
    {
        public override string ID => nameof(LoadingScreenView);

        public LoadingPage(DiContainer container, UIManager uiManager, ILogger logger) : base(container, uiManager, logger)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            this.View.BtnNewGame.onClick.AddListener(this.OnClickBtnNewGame);
            this.View.BtnSetting.onClick.AddListener(this.OnClickBtnSetting);
            this.View.BtnExit.onClick.AddListener(this.OnClickBtnExit);
        }


        private void OnClickBtnNewGame()
        {
        }

        private void OnClickBtnSetting()
        {
            this.UIManager.OpenScreen<SettingPopup>();
        }

        private void OnClickBtnExit()
        {
            UnityUtils.QuitApplication();
        }
    }
}