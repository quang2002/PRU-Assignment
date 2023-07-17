namespace Models.Blueprint
{
    using System;
    using GDK.AssetsManager.Scripts;
    using GDK.BlueprintManager.Scripts;
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

            [JsonProperty("effects")]
            public EffectRecord[] SkillEffects { get; private set; }
        }

        [Serializable]
        public class EffectRecord
        {
            [JsonProperty("id")]
            public string ID { get; private set; }

            [JsonProperty("value")]
            public string Value { get; private set; }

            [JsonProperty("target")]
            public EffectTarget Target { get; private set; }

            [JsonProperty("duration")]
            public float Duration { get; private set; }
        }

        [Serializable]
        public enum EffectTarget
        {
            None,
            Self,
            Enemy,
            Ally,
            AllEnemies,
            AllAllies,
            All,
        }
    }
}