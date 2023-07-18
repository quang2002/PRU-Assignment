namespace GameplayScene.Screens.Components
{
    using System;
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
        private UpgradeBlueprint        UpgradeBlueprint        { get; set; }

        [Inject]
        private void Inject(MainLocalDataController mainLocalDataController,
                            UpgradeBlueprint        upgradeBlueprint)
        {
            this.MainLocalDataController = mainLocalDataController;
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
            var coinNeeded = this.CoinPerLevel * this.CurrentStatLevel;

            var newCoins = this.MainLocalDataController.Coins - coinNeeded;

            if (newCoins < 0) return;

            this.MainLocalDataController.SetCoins(newCoins);

            this.UpgradeSkill();
            this.RebindUpgrade();
        }

        private void RebindUpgrade()
        {
            var coinNeeded = this.CoinPerLevel * this.CurrentStatLevel;

            this.TextCoinNeeded.text     = coinNeeded.ToShortString();
            this.BtnUpgrade.interactable = coinNeeded <= this.MainLocalDataController.Coins;
        }

        private void UpgradeSkill()
        {
            this.MainLocalDataController.LevelUp(this.StatType);
        }

        private long CoinPerLevel => this.StatType switch
        {
            StatType.Attack         => this.UpgradeBlueprint.Attack.Coin,
            StatType.Health         => this.UpgradeBlueprint.Health.Coin,
            StatType.AttackSpeed    => this.UpgradeBlueprint.AttackSpeed.Coin,
            StatType.CriticalRate   => this.UpgradeBlueprint.CriticalRate.Coin,
            StatType.CriticalDamage => this.UpgradeBlueprint.CriticalDamage.Coin,
            _                       => throw new ArgumentOutOfRangeException()
        };

        private long CurrentStatLevel => this.StatType switch
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