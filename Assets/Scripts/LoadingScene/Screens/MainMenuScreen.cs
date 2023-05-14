namespace LoadingScene.Screens
{
    using GDK.GDKUtils.Scripts;
    using GDK.UIManager.Scripts;
    using UnityEngine;
    using UnityEngine.UI;

    public class MainMenuScreen : BasePage
    {
        #region View

        [field: SerializeField]
        public Button BtnNewGame { get; private set; }

        [field: SerializeField]
        public Button BtnSetting { get; private set; }

        [field: SerializeField]
        public Button BtnExit { get; private set; }

        #endregion

        protected override void OnInit()
        {
            base.OnInit();

            this.BtnNewGame.onClick.AddListener(this.OnClickBtnNewGame);
            this.BtnSetting.onClick.AddListener(this.OnClickBtnSetting);
            this.BtnExit.onClick.AddListener(this.OnClickBtnExit);
        }


        private void OnClickBtnNewGame()
        {
        }

        private void OnClickBtnSetting()
        {
            this.UIManager.OpenScreen<SettingScreen>();
        }

        private void OnClickBtnExit() => this.QuitApplication();
    }
}