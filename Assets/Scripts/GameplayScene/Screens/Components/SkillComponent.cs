namespace GameplayScene.Screens.Components
{
    using GameplayScene.Ability.System;
    using GDK.AssetsManager;
    using GDK.UIManager;
    using Models.DataControllers;
    using Signals;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class SkillComponent : MonoBehaviour
    {
        #region Serialize Fields

        [field: SerializeField]
        private uint SlotID { get; set; }

        [field: SerializeField]
        private GameObject Mask { get; set; }

        [field: SerializeField]
        private Image SkillImage { get; set; }

        [field: SerializeField]
        private Image CooldownMask { get; set; }

        #endregion

        #region Inject

        private IAssetsManager          AssetsManager           { get; set; }
        private SignalBus               SignalBus               { get; set; }
        private IAbilitySystem          AbilitySystem           { get; set; }
        private InventoryDataController InventoryDataController { get; set; }
        private UIManager               UIManager               { get; set; }

        [Inject]
        private void Inject(IAssetsManager          assetsManager,
                            SignalBus               signalBus,
                            IAbilitySystem          abilitySystem,
                            InventoryDataController inventoryDataController,
                            UIManager               uiManager)
        {
            this.AssetsManager           = assetsManager;
            this.SignalBus               = signalBus;
            this.AbilitySystem           = abilitySystem;
            this.InventoryDataController = inventoryDataController;
            this.UIManager               = uiManager;
        }

        #endregion

        private float CooldownFillAmount
        {
            get => this.CooldownMask.fillAmount;
            set => this.CooldownMask.fillAmount = value;
        }

        public BaseSkill Skill { get; private set; }

        public void ReloadData()
        {
            var skillID = this.InventoryDataController.GetSkillAtSlot(this.SlotID)?.ID;

            if (skillID is null || (this.Skill = this.AbilitySystem.GetSkill(skillID)) is null)
            {
                this.Mask.SetActive(false);
                this.SkillImage.sprite = null;
                return;
            }

            var sprite = this.AssetsManager.LoadSprite(this.Skill.SkillRecord.Icon);

            this.Mask.SetActive(sprite);
            this.SkillImage.sprite = sprite;
        }

        private void Start()
        {
            this.ReloadData();
            this.SignalBus.Subscribe<EquippedSkillSignal>(this.OnEquippedSkill);
            this.GetComponent<Button>().onClick.AddListener(this.OnClickSkillButton);
        }

        private void OnDestroy()
        {
            this.SignalBus.Unsubscribe<EquippedSkillSignal>(this.OnEquippedSkill);
        }

        private void Update()
        {
            if (this.Skill is null)
            {
                return;
            }

            this.CooldownFillAmount = this.Skill.CooldownPercent;
        }

        private void OnEquippedSkill(EquippedSkillSignal obj)
        {
            if (obj.Slot != this.SlotID)
            {
                return;
            }

            this.ReloadData();
        }

        private void OnClickSkillButton()
        {
            if (this.Skill is null)
            {
                this.UIManager.OpenScreen<SkillSettingScreen>();
            }
            else
            {
                this.Skill.Cast();
            }
        }
    }
}