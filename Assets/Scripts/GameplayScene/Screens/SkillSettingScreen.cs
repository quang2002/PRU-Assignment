namespace GameplayScene.Screens
{
    using GDK.UIManager;
    using Models.DataControllers;
    using UnityEngine;

    public class SkillSettingScreen : BasePopup
    {
        #region View
        
        [field: SerializeField]
        public Transform EquippedSkillListTransform { get; private set; }
        
        [field: SerializeField]
        public Transform SkillListTransform { get; private set; }

        #endregion

        #region Inject

        private InventoryDataController InventoryDataController { get; set; }

        #endregion

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnShow()
        {
            base.OnShow();
        }
    }
}