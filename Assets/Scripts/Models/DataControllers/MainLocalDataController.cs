namespace Models.DataControllers
{
    using System.Numerics;
    using GDK.LocalData;
    using Models.LocalData;

    public class MainLocalDataController : ILocalDataController
    {
        #region Inject

        private MainLocalData MainLocalData { get; }

        public MainLocalDataController(MainLocalData mainLocalData)
        {
            this.MainLocalData = mainLocalData;
        }

        #endregion

        public string Username => this.MainLocalData.Username;

        public void SetUsername(string username)
        {
            this.MainLocalData.Username = username;
        }

        public BigInteger Coins => this.MainLocalData.Coins;

        public void SetCoins(BigInteger value)
        {
            this.MainLocalData.Coins = value;
        }

        public UpgradeData UpgradeData => this.MainLocalData.UpgradeData;

        public void LevelUpAttack()
        {
            var upgradeData = this.MainLocalData.UpgradeData;
            upgradeData.AttackLevel++;
            this.MainLocalData.UpgradeData = upgradeData;
        }

        public void LevelUpHealth()
        {
            var upgradeData = this.MainLocalData.UpgradeData;
            upgradeData.HealthLevel++;
            this.MainLocalData.UpgradeData = upgradeData;
        }

        public void LevelUpAttackSpeed()
        {
            var upgradeData = this.MainLocalData.UpgradeData;
            upgradeData.AttackSpeedLevel++;
            this.MainLocalData.UpgradeData = upgradeData;
        }

        public void LevelUpCriticalRate()
        {
            var upgradeData = this.MainLocalData.UpgradeData;
            upgradeData.CriticalRateLevel++;
            this.MainLocalData.UpgradeData = upgradeData;
        }

        public void LevelUpCriticalDamage()
        {
            var upgradeData = this.MainLocalData.UpgradeData;
            upgradeData.CriticalDamageLevel++;
            this.MainLocalData.UpgradeData = upgradeData;
        }
    }
}