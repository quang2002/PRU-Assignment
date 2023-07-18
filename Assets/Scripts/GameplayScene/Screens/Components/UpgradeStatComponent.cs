namespace GameplayScene.Screens.Components
{
    using System;
    using System.Numerics;
    using Models.Blueprint;
    using Models.Common;
    using Models.DataControllers;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Utilities;
    using Zenject;

    public class UpgradeStatComponent : MonoBehaviour
    {
        #region Serialize Fields

        [field: SerializeField]
        public StatType StatType { get; private set; }

        [field: SerializeField]
        public Button BtnUpgrade { get; private set; }

        [field: SerializeField]
        public TMP_Text TextCoinNeeded { get; private set; }

        [field: SerializeField]
        public TMP_Text TextCurrentValue { get; private set; }

        #endregion

        #region Inject

        private MainLocalDataController MainLocalDataController { get; set; }
        private InventoryDataController InventoryDataController { get; set; }
        private UpgradeBlueprint        UpgradeBlueprint        { get; set; }

        [Inject]
        private void Inject(MainLocalDataController mainLocalDataController,
                            InventoryDataController inventoryDataController,
                            UpgradeBlueprint        upgradeBlueprint)
        {
            this.MainLocalDataController = mainLocalDataController;
            this.InventoryDataController = inventoryDataController;
            this.UpgradeBlueprint        = upgradeBlueprint;
        }

        #endregion

        private void Start()
        {
            this.BtnUpgrade.onClick.RemoveAllListeners();
            this.BtnUpgrade.onClick.AddListener(this.OnClickUpgrade);

            this.RebindUpgrade();
        }

        private void OnClickUpgrade()
        {
            var coinNeeded = this.CurrentLevel * (BigInteger)this.BlueprintCoin;

            var postCoins = this.MainLocalDataController.Coins - coinNeeded;

            if (postCoins < 0) return;

            this.MainLocalDataController.SetCoins(postCoins);

            this.UpgradeSkill();
            this.RebindUpgrade();
        }

        private void RebindUpgrade()
        {
            var coinNeeded = this.CurrentLevel * (BigInteger)this.BlueprintCoin;

            this.TextCoinNeeded.text     = coinNeeded.ToShortString();
            this.BtnUpgrade.interactable = coinNeeded <= this.MainLocalDataController.Coins;
        }

        private void UpgradeSkill()
        {
            Action upgradeAction = this.StatType switch
            {
                StatType.Attack         => this.MainLocalDataController.LevelUpAttack,
                StatType.Health         => this.MainLocalDataController.LevelUpHealth,
                StatType.AttackSpeed    => this.MainLocalDataController.LevelUpAttackSpeed,
                StatType.CriticalRate   => this.MainLocalDataController.LevelUpCriticalRate,
                StatType.CriticalDamage => this.MainLocalDataController.LevelUpCriticalDamage,
                _                       => throw new ArgumentOutOfRangeException()
            };

            upgradeAction();
        }

        private uint CurrentLevel => this.StatType switch
        {
            StatType.Attack         => this.UpgradeBlueprint.Attack.Coin,
            StatType.Health         => this.UpgradeBlueprint.Health.Coin,
            StatType.AttackSpeed    => this.UpgradeBlueprint.AttackSpeed.Coin,
            StatType.CriticalRate   => this.UpgradeBlueprint.CriticalRate.Coin,
            StatType.CriticalDamage => this.UpgradeBlueprint.CriticalDamage.Coin,
            _                       => throw new ArgumentOutOfRangeException()
        };

        private uint BlueprintCoin => this.StatType switch
        {
            StatType.Attack         => this.MainLocalDataController.UpgradeData.AttackLevel,
            StatType.Health         => this.MainLocalDataController.UpgradeData.HealthLevel,
            StatType.AttackSpeed    => this.MainLocalDataController.UpgradeData.AttackSpeedLevel,
            StatType.CriticalRate   => this.MainLocalDataController.UpgradeData.CriticalRateLevel,
            StatType.CriticalDamage => this.MainLocalDataController.UpgradeData.CriticalDamageLevel,
            _                       => throw new ArgumentOutOfRangeException()
        };
    }
}