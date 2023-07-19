namespace Models.DataControllers
{
    using System;
    using Common;
    using GDK.LocalData;
    using Models.Blueprint;
    using Models.LocalData;

    public class MainLocalDataController : ILocalDataController
    {
        #region Inject

        private MainLocalData    MainLocalData    { get; }
        public  UpgradeBlueprint UpgradeBlueprint { get; }

        public MainLocalDataController(MainLocalData    mainLocalData,
                                       UpgradeBlueprint upgradeBlueprint)
        {
            this.MainLocalData    = mainLocalData;
            this.UpgradeBlueprint = upgradeBlueprint;
        }

        #endregion

        public string Username => this.MainLocalData.Username;

        public void SetUsername(string username)
        {
            this.MainLocalData.Username = username;
        }

        public float MaxHealth => this.UpgradeBlueprint.Health.Value * this.MainLocalData.UpgradeData.HealthLevel;

        public long Coins => this.MainLocalData.Coins;

        public void SetCoins(long value)
        {
            this.MainLocalData.Coins = value;
        }

        public UpgradeData UpgradeData => this.MainLocalData.UpgradeData;

        public void LevelUp(StatType statType)
        {
            var upgradeData = this.MainLocalData.UpgradeData;

            switch (statType)
            {
                case StatType.Attack:
                    upgradeData.AttackLevel++;
                    break;
                case StatType.Health:
                    upgradeData.HealthLevel++;
                    break;
                case StatType.AttackSpeed:
                    upgradeData.AttackSpeedLevel++;
                    break;
                case StatType.CriticalRate:
                    upgradeData.CriticalRateLevel++;
                    break;
                case StatType.CriticalDamage:
                    upgradeData.CriticalDamageLevel++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(statType), statType, null);
            }

            this.MainLocalData.UpgradeData = upgradeData;
        }
    }
}