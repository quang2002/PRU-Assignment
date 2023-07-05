namespace Models.Blueprint
{
    using GDK.AssetsManager.Scripts;
    using GDK.BlueprintManager.Scripts;

    public class UpgradeBlueprint : JsonBlueprint
    {
        public override string AddressableKey => nameof(UpgradeBlueprint);

        public UpgradeBlueprint(IAssetsManager assetsManager) : base(assetsManager)
        {
        }

        public uint  AttackCoinIncrementRate  { get; set; }
        public float AttackValueIncrementRate { get; set; }

        public uint  HealthCoinIncrementRate  { get; set; }
        public float HealthValueIncrementRate { get; set; }

        public uint  AttackSpeedCoinIncrementRate  { get; set; }
        public float AttackSpeedValueIncrementRate { get; set; }

        public uint  CriticalRateCoinIncrementRate  { get; set; }
        public float CriticalRateValueIncrementRate { get; set; }

        public uint  CriticalDamageCoinIncrementRate  { get; set; }
        public float CriticalDamageValueIncrementRate { get; set; }
    }
}