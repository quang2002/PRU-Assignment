namespace GameplayScene.Screens
{

    using System;
    using System.Numerics;
    using GDK.UIManager.Scripts;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Utilities;

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
        }

        private void OnClickShopPopup()
        {
            // this.UIManager.OpenScreen<>();
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

        private void OnClickUpgradeCriticalDamage()
        {
        }

        private void OnClickUpgradeCriticalRate()
        {
        }

        private void OnClickUpgradeAttackSpeed()
        {
        }

        private void OnClickUpgradeHealth()
        {
        }

        private void OnClickUpgradeAttack()
        {
        }

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