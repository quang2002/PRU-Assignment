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

    public class SkillItemView : MonoBehaviour
    {
        #region View

        [field: SerializeField]
        public Button ButtonEquip { get; private set; }
        
        [field: SerializeField]
        public Image SkillImage { get; private set; }
        
        [field: SerializeField]
        public TextMeshProUGUI SkillLevel { get; private set; }

        #endregion

        #region Inject

        private InventoryDataController InventoryDataController { get; set; }
        private SkillBlueprint          SkillBlueprint          { get; set; }
        private AssetsManager           AssetsManager           { get; set; }

        [Inject]
        private void Inject(InventoryDataController inventoryDataController,
                            SkillBlueprint skillBlueprint,
                            AssetsManager assetsManager)
        {
            this.InventoryDataController = inventoryDataController;
            this.SkillBlueprint          = skillBlueprint;
            this.AssetsManager           = assetsManager;
        }

        #endregion
        private       SkillData skillData;
        private const string    LevelPrefix = "LEVEL";
        
        public void BindData(SkillData data)
        {
            this.skillData = data;
            this.ButtonEquip.onClick.AddListener(this.EquipSkill);
            this.SkillImage.sprite = this.AssetsManager.LoadSprite(this.SkillBlueprint[this.skillData.ID].Icon);
            this.SkillLevel.text        = $"{LevelPrefix}{this.skillData.Level}";
        }

        private void EquipSkill()
        {
            uint slot = 6;
            for (uint i = 0; i < this.InventoryDataController.InventoryLocalData.EquippedSkill.Length; i++)
            {
                if (this.InventoryDataController.InventoryLocalData.EquippedSkill[i] != null) continue;
                slot = i;
                break;
            }
            this.InventoryDataController.UseSkillAtSlot(this.skillData.ID,
                                                        slot);
        }
    }
}