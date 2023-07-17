namespace GameplayScene.Screens.Components
{
    using GDK.AssetsManager.Scripts;
    using Models.Blueprint;
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

        [Inject]
        private IAssetsManager AssetsManager { get; set; }

        [Inject]
        private SignalBus SignalBus { get; set; }

        [Inject]
        private SkillBlueprint SkillBlueprint { get; set; }

        [Inject]
        private InventoryDataController InventoryDataController { get; set; }

        #endregion

        private float CooldownFillAmount
        {
            get => this.CooldownMask.fillAmount;
            set => this.CooldownMask.fillAmount = value;
        }

        public SkillBlueprint.SkillRecord SkillRecord { get; private set; }

        public void ReloadData()
        {
            var skillID = this.InventoryDataController.GetSkillAtSlot(this.SlotID)?.ID;

            if (skillID is null || (this.SkillRecord = this.SkillBlueprint[skillID]) is null)
            {
                this.Mask.SetActive(false);
                this.SkillImage.sprite = null;
                return;
            }

            var sprite = this.AssetsManager.LoadSprite(this.SkillRecord.Icon);

            this.Mask.SetActive(sprite);
            this.SkillImage.sprite = sprite;
        }

        private void Start()
        {
            this.ReloadData();
            this.SignalBus.Subscribe<EquippedSkillSignal>(this.OnEquippedSkill);
        }

        private void OnDestroy()
        {
            this.SignalBus.Unsubscribe<EquippedSkillSignal>(this.OnEquippedSkill);
        }

        private void OnEquippedSkill(EquippedSkillSignal obj)
        {
            if (obj.Slot != this.SlotID)
            {
                return;
            }

            this.ReloadData();
        }

        private void Update()
        {
            if (this.SkillRecord is null)
            {
                return;
            }
        }
    }
}