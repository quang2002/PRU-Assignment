namespace GameplayScene.Screens.Adapter
{
    using System.Linq;
    using GDK.AssetsManager;
    using Models.Blueprint;
    using Models.DataControllers;
    using Models.LocalData;
    using Signals;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class SkillItemView : MonoBehaviour
    {
        #region View

        [field: SerializeField]
        public Button ButtonEquip { get; private set; }

        [field: SerializeField]
        public Image SkillImage { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI SkillLevel { get; private set; }

        [field: SerializeField]
        public Image CoverImage { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI EquippedText { get; private set; }

        #endregion

        #region Inject

        private InventoryDataController InventoryDataController { get; set; }
        private SkillBlueprint          SkillBlueprint          { get; set; }
        private IAssetsManager          AssetsManager           { get; set; }
        private SignalBus               SignalBus               { get; set; }

        [Inject]
        private void Inject(InventoryDataController inventoryDataController,
                            SkillBlueprint          skillBlueprint,
                            IAssetsManager          assetsManager,
                            SignalBus               signalBus)
        {
            this.InventoryDataController = inventoryDataController;
            this.SkillBlueprint          = skillBlueprint;
            this.AssetsManager           = assetsManager;
            this.SignalBus               = signalBus;
        }

        #endregion

        private       SkillData skillData;
        private const string    LevelPrefix = "";

        public void BindData(SkillData data)
        {
            this.skillData = data;
            this.ButtonEquip.onClick.AddListener(this.EquipSkill);
            this.SkillImage.sprite = this.AssetsManager.LoadSprite(this.SkillBlueprint[this.skillData.ID].Icon);
            this.SkillLevel.text   = $"{LevelPrefix}{this.skillData.Level}";

            this.SetEquipped(this.InventoryDataController.IsSkillEquipped(data.ID));
        }

        private void SetEquipped(bool isEquipped)
        {
            this.CoverImage.gameObject.SetActive(isEquipped);
            this.EquippedText.gameObject.SetActive(isEquipped);
        }

        private void EquipSkill()
        {
            this.InventoryDataController.UseSkill(this.skillData.ID);
        }
    }
}