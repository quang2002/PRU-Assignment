namespace Models.DataControllers
{
    using System;
    using Common;
    using GDK.LocalData;
    using Models.Blueprint;
    using Models.LocalData;
    using Signals;
    using Zenject;

    public class MainLocalDataController : ILocalDataController
    {
        #region Inject

        private MainLocalData    MainLocalData    { get; }
        public  UpgradeBlueprint UpgradeBlueprint { get; }
        public  SignalBus        SignalBus        { get; }

        public MainLocalDataController(MainLocalData    mainLocalData,
                                       UpgradeBlueprint upgradeBlueprint,
                                       SignalBus        signalBus)
        {
            this.MainLocalData    = mainLocalData;
            this.UpgradeBlueprint = upgradeBlueprint;
            this.SignalBus        = signalBus;

            this.SignalBus.Subscribe<EnemyDeadSignal>(this.OnEnemyDead);
        }

        #endregion

        public string Username => this.MainLocalData.Username;

        public void SetUsername(string username)
        {
            this.MainLocalData.Username = username;
        }

        private void OnEnemyDead(EnemyDeadSignal signal)
        {
            this.Exp   += signal.EnemyRecord.Exp;
            this.Coins += signal.EnemyRecord.Coins;
        }

        public long Coins
        {
            get => this.MainLocalData.Coins;
            set
            {
                if (this.MainLocalData.Coins != value)
                {
                    this.SignalBus.Fire(new CoinChangedSignal
                    {
                        OldCoins = this.MainLocalData.Coins,
                        NewCoins = value
                    });
                }

                this.MainLocalData.Coins = value;
            }
        }

        public uint Exp
        {
            get => this.MainLocalData.Exp;
            set => this.MainLocalData.Exp = value;
        }

        public uint Level => this.Exp / 100;

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

        public float GetStatValue(StatType statType)
        {
            var upgradeBlueprint = this.UpgradeBlueprint;
            var mainLocalData    = this.MainLocalData;

            return statType switch
            {
                StatType.Attack         => upgradeBlueprint.Attack.Value * mainLocalData.UpgradeData.AttackLevel + upgradeBlueprint.Attack.BaseValue,
                StatType.Health         => upgradeBlueprint.Health.Value * mainLocalData.UpgradeData.HealthLevel + upgradeBlueprint.Health.BaseValue,
                StatType.AttackSpeed    => upgradeBlueprint.AttackSpeed.Value * mainLocalData.UpgradeData.AttackSpeedLevel + upgradeBlueprint.AttackSpeed.BaseValue,
                StatType.CriticalRate   => upgradeBlueprint.CriticalRate.Value * mainLocalData.UpgradeData.CriticalRateLevel + upgradeBlueprint.CriticalRate.BaseValue,
                StatType.CriticalDamage => upgradeBlueprint.CriticalDamage.Value * mainLocalData.UpgradeData.CriticalDamageLevel + upgradeBlueprint.CriticalDamage.BaseValue,
                _                       => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
            };
        }
    }
}