namespace Models.Blueprint
{
    using System;
    using GDK.AssetsManager;
    using GDK.BlueprintManager;
    using Models.Common;
    using Newtonsoft.Json;

    public class UpgradeBlueprint : JsonObjectBlueprint<UpgradeBlueprint.UpgradeRecord>
    {
        public override string AddressableKey => nameof(UpgradeBlueprint);

        public UpgradeBlueprint(IAssetsManager assetsManager) : base(assetsManager)
        {
        }

        public UpgradeRecord Attack         => this[StatTypes.Attack];
        public UpgradeRecord Health         => this[StatTypes.Health];
        public UpgradeRecord AttackSpeed    => this[StatTypes.AttackSpeed];
        public UpgradeRecord CriticalRate   => this[StatTypes.CriticalRate];
        public UpgradeRecord CriticalDamage => this[StatTypes.CriticalDamage];

        [Serializable]
        public class UpgradeRecord
        {
            [JsonProperty("coin")]
            public uint Coin { get; private set; }

            [JsonProperty("value")]
            public float Value { get; private set; }
        }
    }
}