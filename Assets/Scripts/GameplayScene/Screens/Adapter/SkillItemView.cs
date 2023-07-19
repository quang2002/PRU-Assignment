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
        private IAssetsManager           AssetsManager           { get; set; }
        private SignalBus               SignalBus               { get; set; }

        [Inject]
        private void Inject(InventoryDataController inventoryDataController,
                            SkillBlueprint skillBlueprint,
                            IAssetsManager assetsManager,
                            SignalBus signalBus)
        {
            this.InventoryDataController = inventoryDataController;
            this.SkillBlueprint          = skillBlueprint;
            this.AssetsManager           = assetsManager;
            this.SignalBus               = signalBus;
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
            this.CoverImage.gameObject.SetActive(this.InventoryDataController.InventoryLocalData.EquippedSkill.Contains(this.skillData.ID));
            this.EquippedText.gameObject.SetActive(this.InventoryDataController.InventoryLocalData.EquippedSkill.Contains(this.skillData.ID));
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
            
            this.InventoryDataController.UseSkillAtSlot(this.skillData.ID, slot);
        }
    }
}