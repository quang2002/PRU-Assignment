namespace GameplayScene.Screens
{
    using GDK.GDKUtils;
    using GDK.UIManager;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameplayScreen : BasePage
    {
        #region View

        [field: SerializeField]
        public Button BtnTalent { get; private set; }

        [field: SerializeField]
        public Button BtnSkill { get; private set; }

        [field: SerializeField]
        public Button BtnArtifact { get; private set; }

        [field: SerializeField]
        public Button BtnShop { get; private set; }

        [field: SerializeField]
        public Button BtnExit { get; private set; }

        #endregion

        protected override void OnInit()
        {
            base.OnInit();

            this.BtnTalent.onClick.AddListener(this.OnClickTalentPopup);
            this.BtnSkill.onClick.AddListener(this.OnClickSkillPopup);
            this.BtnArtifact.onClick.AddListener(this.OnClickArtifactPopup);
            this.BtnShop.onClick.AddListener(this.OnClickShopPopup);
            this.BtnExit.onClick.AddListener(() => this.QuitApplication());
        }

        protected override void OnShow()
        {
            base.OnShow();
        }

        #region On Click Events

        private void OnClickShopPopup()
        {
            this.UIManager.OpenScreen<GachaScreen>();
        }

        private void OnClickArtifactPopup()
        {
            // this.UIManager.OpenScreen<>();
        }

        private void OnClickSkillPopup()
        {
            // this.UIManager.OpenScreen<>();
        }

        private void OnClickTalentPopup()
        {
            // this.UIManager.OpenScreen<>();
        }

        #endregion
    }
}