namespace Models.Blueprint
{
    using System;
    using GDK.AssetsManager;
    using GDK.BlueprintManager;
    using Newtonsoft.Json;

    public class EnemyBlueprint : JsonObjectBlueprint<EnemyBlueprint.EnemyRecord>
    {
        public EnemyBlueprint(IAssetsManager assetsManager) : base(assetsManager)
        {
        }

        public override string AddressableKey => nameof(EnemyBlueprint);

        [Serializable]
        public class EnemyRecord
        {
            [JsonProperty("name")]
            public string Name { get; private set; }

            [JsonProperty("animator")]
            public string AnimatorController { get; private set; }

            [JsonProperty("damage")]
            public uint BaseDamage { get; private set; }

            [JsonProperty("health")]
            public uint BaseHealth { get; private set; }

            [JsonProperty("damage-inc")]
            public uint DamageInc { get; private set; }

            [JsonProperty("health-inc")]
            public uint HealthInc { get; private set; }
        }
    }
}