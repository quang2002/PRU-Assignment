namespace GameplayScene.Screens.Adapter
{
    using System.Linq;
    using GDK.AssetsManager;
    using Models.Blueprint;
    using Models.DataControllers;
    using Models.LocalData;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class EquippedItemView : MonoBehaviour
    {
        #region View

        [field: SerializeField]
        public Button ButtonUnEquip { get; private set; }

        [field: SerializeField]
        public Image SkillImage { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI SkillLevel { get; private set; }

        #endregion

        #region Inject

        private InventoryDataController InventoryDataController { get; set; }
        private SkillBlueprint          SkillBlueprint          { get; set; }
        private IAssetsManager          AssetsManager           { get; set; }

        [Inject]
        private void Inject(InventoryDataController inventoryDataController,
                            SkillBlueprint          skillBlueprint,
                            IAssetsManager          assetsManager)
        {
            this.InventoryDataController = inventoryDataController;
            this.SkillBlueprint          = skillBlueprint;
            this.AssetsManager           = assetsManager;
        }

        #endregion

        private       string skillId;
        private const string LevelPrefix = "";

        public void BindData(SkillData skillData)
        {
            this.skillId = skillData.ID;
            this.ButtonUnEquip.onClick.AddListener(this.UnEquipSkill);
            this.SkillImage.sprite = this.AssetsManager.LoadSprite(this.SkillBlueprint[this.skillId].Icon);
            this.SkillLevel.text   = $"{LevelPrefix}{skillData.Level}";
        }

        private void UnEquipSkill()
        {
            this.InventoryDataController.RemoveSkill(this.skillId);
        }
    }
}