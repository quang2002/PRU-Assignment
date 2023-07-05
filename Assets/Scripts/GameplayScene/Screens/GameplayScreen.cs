namespace GameplayScene.Screens
{
    using System;
    using System.Numerics;
    using GDK.GDKUtils.Scripts;
    using GDK.UIManager.Scripts;
    using Models.Blueprint;
    using Models.DataControllers;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Utilities;
    using Zenject;

    public class GameplayScreen : BasePage
    {
        #region View

        [field: SerializeField]
        public Button BtnTalent { get; private set; }

        [field: SerializeField]
        public Button BtnSkill { get; private set; }

        [field: SerializeField]
        public Button BtnArtifact { get; private set; }

        [field: SerializeField]
        public Button BtnShop { get; private set; }

        [field: SerializeField]
        public Button BtnExit { get; private set; }

        [field: SerializeField]
        public UpgradeComponent UpgradeAttack { get; private set; }

        [field: SerializeField]
        public UpgradeComponent UpgradeHealth { get; private set; }

        [field: SerializeField]
        public UpgradeComponent UpgradeAttackSpeed { get; private set; }

        [field: SerializeField]
        public UpgradeComponent UpgradeCriticalRate { get; private set; }

        [field: SerializeField]
        public UpgradeComponent UpgradeCriticalDamage { get; private set; }

        #endregion

        #region Inject

        [Inject]
        private MainLocalDataController MainLocalDataController { get; set; }

        [Inject]
        private UpgradeBlueprint UpgradeBlueprint { get; set; }

        #endregion

        protected override void OnInit()
        {
            base.OnInit();
            this.UpgradeAttack.SetOnClickUpgradeButton(this.OnClickUpgradeAttack);
            this.UpgradeHealth.SetOnClickUpgradeButton(this.OnClickUpgradeHealth);
            this.UpgradeAttackSpeed.SetOnClickUpgradeButton(this.OnClickUpgradeAttackSpeed);
            this.UpgradeCriticalRate.SetOnClickUpgradeButton(this.OnClickUpgradeCriticalRate);
            this.UpgradeCriticalDamage.SetOnClickUpgradeButton(this.OnClickUpgradeCriticalDamage);

            this.BtnTalent.onClick.AddListener(this.OnClickTalentPopup);
            this.BtnSkill.onClick.AddListener(this.OnClickSkillPopup);
            this.BtnArtifact.onClick.AddListener(this.OnClickArtifactPopup);
            this.BtnShop.onClick.AddListener(this.OnClickShopPopup);
            this.BtnExit.onClick.AddListener(() => this.QuitApplication());
        }

        protected override void OnShow()
        {
            base.OnShow();
            this.RebindUpgradeAttack();
            this.RebindUpgradeHealth();
            this.RebindUpgradeAttackSpeed();
            this.RebindUpgradeCriticalRate();
            this.RebindUpgradeCriticalDamage();
        }

        private void OnClickShopPopup()
        {
            this.UIManager.OpenScreen<GachaScreen>();
        }

        private void OnClickArtifactPopup()
        {
            // this.UIManager.OpenScreen<>();
        }

        private void OnClickSkillPopup()
        {
            // this.UIManager.OpenScreen<>();
        }

        private void OnClickTalentPopup()
        {
            // this.UIManager.OpenScreen<>();
        }

        private void OnClickUpgradeCriticalDamage() => this.InternalOnClickUpgrade(this.CoinNeededCriticalDamage, this.MainLocalDataController.LevelUpCriticalDamage, this.RebindUpgradeCriticalDamage);

        private void OnClickUpgradeCriticalRate() => this.InternalOnClickUpgrade(this.CoinNeededCriticalRate, this.MainLocalDataController.LevelUpCriticalRate, this.RebindUpgradeCriticalRate);

        private void OnClickUpgradeAttackSpeed() => this.InternalOnClickUpgrade(this.CoinNeededAttackSpeed, this.MainLocalDataController.LevelUpAttackSpeed, this.RebindUpgradeAttackSpeed);

        private void OnClickUpgradeHealth() => this.InternalOnClickUpgrade(this.CoinNeededHealth, this.MainLocalDataController.LevelUpHealth, this.RebindUpgradeHealth);

        private void OnClickUpgradeAttack() => this.InternalOnClickUpgrade(this.CoinNeededAttack, this.MainLocalDataController.LevelUpAttack, this.RebindUpgradeAttack);

        private void InternalOnClickUpgrade(BigInteger coinNeeded, Action levelUpAction, Action rebindAction)
        {
            var postCoins = this.MainLocalDataController.Coins - coinNeeded;

            if (postCoins < 0) return;

            this.MainLocalDataController.SetCoins(postCoins);
            levelUpAction.Invoke();
            rebindAction.Invoke();
        }

        #region Bind UI Values

        private void RebindUpgradeAttack()         => this.InternalRebindUpgrade(this.CoinNeededAttack, this.UpgradeAttack);
        private void RebindUpgradeHealth()         => this.InternalRebindUpgrade(this.CoinNeededHealth, this.UpgradeHealth);
        private void RebindUpgradeAttackSpeed()    => this.InternalRebindUpgrade(this.CoinNeededAttackSpeed, this.UpgradeAttackSpeed);
        private void RebindUpgradeCriticalRate()   => this.InternalRebindUpgrade(this.CoinNeededCriticalRate, this.UpgradeCriticalRate);
        private void RebindUpgradeCriticalDamage() => this.InternalRebindUpgrade(this.CoinNeededCriticalDamage, this.UpgradeCriticalDamage);

        private void InternalRebindUpgrade(BigInteger coinNeeded, UpgradeComponent upgradeComponent)
        {
            upgradeComponent.SetCoinNeeded(coinNeeded);
            upgradeComponent.SetUpgradeButtonState(coinNeeded <= this.MainLocalDataController.Coins);
        }

        public BigInteger CoinNeededAttack         => this.MainLocalDataController.UpgradeData.AttackLevel * this.UpgradeBlueprint.AttackCoinIncrementRate;
        public BigInteger CoinNeededHealth         => this.MainLocalDataController.UpgradeData.HealthLevel * this.UpgradeBlueprint.HealthCoinIncrementRate;
        public BigInteger CoinNeededAttackSpeed    => this.MainLocalDataController.UpgradeData.AttackSpeedLevel * this.UpgradeBlueprint.AttackSpeedCoinIncrementRate;
        public BigInteger CoinNeededCriticalRate   => this.MainLocalDataController.UpgradeData.CriticalRateLevel * this.UpgradeBlueprint.CriticalRateCoinIncrementRate;
        public BigInteger CoinNeededCriticalDamage => this.MainLocalDataController.UpgradeData.CriticalDamageLevel * this.UpgradeBlueprint.CriticalDamageCoinIncrementRate;

        #endregion

        [Serializable]
        public class UpgradeComponent
        {
            [field: SerializeField]
            public Button BtnUpgrade { get; private set; }

            [field: SerializeField]
            public TMP_Text TextCoinNeeded { get; private set; }

            [field: SerializeField]
            public TMP_Text TextCurrentValue { get; private set; }

            public void SetCoinNeeded(BigInteger value)
            {
                this.TextCoinNeeded.text = value.ToShortString();
            }

            public void SetCurrentValue(BigInteger value)
            {
                this.TextCurrentValue.text = value.ToShortString();
            }

            public void SetUpgradeButtonState(bool interactable)
            {
                this.BtnUpgrade.interactable = interactable;
            }

            public void SetOnClickUpgradeButton(Action onClick)
            {
                this.BtnUpgrade.onClick.RemoveAllListeners();
                this.BtnUpgrade.onClick.AddListener(onClick.Invoke);
            }
        }
    }
}