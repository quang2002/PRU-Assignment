namespace Models.Blueprint
{
    using System;
    using GDK.AssetsManager;
    using GDK.BlueprintManager;
    using Newtonsoft.Json;

    public class SkillBlueprint : JsonObjectBlueprint<SkillBlueprint.SkillRecord>
    {
        public override string AddressableKey => nameof(SkillBlueprint);

        public SkillBlueprint(IAssetsManager assetsManager) : base(assetsManager)
        {
        }

        [Serializable]
        public class SkillRecord
        {
            [JsonProperty("name")]
            public string Name { get; private set; }

            [JsonProperty("description")]
            public string Description { get; private set; }

            [JsonProperty("icon")]
            public string Icon { get; private set; }

            [JsonProperty("cooldown")]
            public float Cooldown { get; private set; }

            [JsonProperty("extras")]
            public dynamic Extras { get; private set; }
        }
    }
}