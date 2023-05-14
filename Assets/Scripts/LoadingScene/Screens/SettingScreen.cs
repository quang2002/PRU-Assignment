namespace LoadingScene.Screens
{
    using GDK.UIManager.Scripts;
    using UnityEngine;
    using UnityEngine.UI;

    public class SettingScreen : BasePopup
    {
        #region View

        [field: SerializeField]
        public Button BtnClose { get; private set; }

        #endregion

        protected override bool IsOverlay => false;


        protected override void OnInit()
        {
            base.OnInit();

            this.BtnClose.onClick.AddListener(this.OnClickBtnClose);
        }

        private void OnClickBtnClose()
        {
            this.Close();
        }
    }
}