namespace GameplayScene.Ability.Effects
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;

    public class DamageEffect : BaseEffect
    {
        public DamageEffect(IAbilitySystem abilitySystem, SkillBlueprint.EffectRecord effectRecord) : base(abilitySystem, effectRecord)
        {
        }

        public override string EffectID => "damage";
    }
}