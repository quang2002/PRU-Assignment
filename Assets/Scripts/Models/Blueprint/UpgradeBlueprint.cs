namespace Models.Blueprint
{
    using System;
    using Common;
    using GDK.AssetsManager;
    using GDK.BlueprintManager;
    using Newtonsoft.Json;

    public class UpgradeBlueprint : JsonObjectBlueprint<UpgradeBlueprint.UpgradeRecord>
    {
        public override string AddressableKey => nameof(UpgradeBlueprint);

        public UpgradeBlueprint(IAssetsManager assetsManager) : base(assetsManager)
        {
        }

        public UpgradeRecord Attack         => this[StatType.Attack];
        public UpgradeRecord Health         => this[StatType.Health];
        public UpgradeRecord HealthSteal    => this[StatType.HealthSteal];
        public UpgradeRecord CriticalRate   => this[StatType.CriticalRate];
        public UpgradeRecord CriticalDamage => this[StatType.CriticalDamage];

        [Serializable]
        public class UpgradeRecord
        {
            [JsonProperty("coin")]
            public long Coin { get; private set; }

            [JsonProperty("value")]
            public float Value { get; private set; }

            [JsonProperty("base")]
            public float BaseValue { get; private set; }
        }
    }
}