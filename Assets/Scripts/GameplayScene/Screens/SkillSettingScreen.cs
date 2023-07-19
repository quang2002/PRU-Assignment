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
        private SignalBus               SignalBus               { get; set; }

        [Inject]
        private void Inject(InventoryDataController inventoryDataController,
                            SignalBus               signalBus)
        {
            this.InventoryDataController = inventoryDataController;
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

        private static void ClearList(Transform listTransform, GameObject skillTemplate)
        {
            foreach (Transform child in listTransform)
            {
                if (child == skillTemplate.transform) continue;
                Destroy(child.gameObject);
            }
        }

        private void ReloadSkill()
        {
            ClearList(this.EquippedSkillListTransform, this.EquippedSkillTemplate);

            ClearList(this.SkillListTransform, this.SkillTemplate);

            foreach (var (_, skillData) in this.InventoryDataController.GetAllEquippedSkillData())
            {
                if (skillData == null) continue;
                var skill = this.Container.InstantiatePrefab(this.EquippedSkillTemplate, this.EquippedSkillListTransform);
                skill.GetComponent<EquippedItemView>().BindData(skillData);
                skill.SetActive(true);
            }

            foreach (var (_, skillData) in this.InventoryDataController.GetAllSkillData())
            {
                var skill = this.Container.InstantiatePrefab(this.SkillTemplate, this.SkillListTransform);
                skill.GetComponent<SkillItemView>().BindData(skillData);
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