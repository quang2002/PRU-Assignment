namespace GameplayScene.Screens
{
    using GameplayScene.Screens.Adapter;
    using GDK.UIManager;
    using Models.DataControllers;
    using Signals;
    using UnityEngine;
    using Zenject;

    public class SkillSettingScreen : BasePopup
    {
        #region View
        
        [field: SerializeField]
        public Transform EquippedSkillListTransform { get; private set; }
        
        [field: SerializeField]
        public Transform SkillListTransform { get; private set; }
        
        [field: SerializeField]
        public GameObject SkillTemplate { get; private set; }
        
        [field: SerializeField]
        public GameObject EquippedSkillTemplate { get; private set; }

        #endregion

        #region Inject

        private InventoryDataController InventoryDataController { get; set; }
        private DiContainer             DiContainer             { get; set; }
        private SignalBus               SignalBus               { get; set; }

        [Inject]
        private void Inject(InventoryDataController inventoryDataController,
                            DiContainer diContainer,
                            SignalBus signalBus)
        {
            this.InventoryDataController = inventoryDataController;
            this.DiContainer             = diContainer;
            this.SignalBus               = signalBus;
        }

        #endregion

        protected override void OnInit()
        {
            base.OnInit();
            
            this.SignalBus.Subscribe<EquippedSkillSignal>(this.ReloadSkill);
        }

        protected override void OnShow()
        {
            base.OnShow();
            
            this.ReloadSkill();
        }

        private void ClearList(Transform listTransform, GameObject skillTemplate)
        {
            foreach (Transform child in listTransform)
            {
                if (child == skillTemplate.transform) continue;
                Destroy(child.gameObject);
            }
        }

        private void ReloadSkill()
        {
            this.ClearList(this.EquippedSkillListTransform, this.EquippedSkillTemplate);

            this.ClearList(this.SkillListTransform, this.SkillTemplate);
            
            foreach (var equippedSkillData in this.InventoryDataController.InventoryLocalData.EquippedSkill)
            {
                if(equippedSkillData == null) continue;
                var skill = Instantiate(this.EquippedSkillTemplate  , this.EquippedSkillListTransform);
                this.DiContainer.InjectGameObject(skill);
                skill.GetComponent<EquippedItemView>().BindData(equippedSkillData);
                skill.SetActive(true);
            }
            
            foreach (var skillData in this.InventoryDataController.InventoryLocalData.SkillData)
            {
                var skill = Instantiate(this.SkillTemplate, this.SkillListTransform);
                this.DiContainer.InjectGameObject(skill);
                skill.GetComponent<SkillItemView>().BindData(skillData.Value);
                skill.SetActive(true);
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            this.SignalBus.Unsubscribe<EquippedSkillSignal>(this.ReloadSkill);
        }
    }
}