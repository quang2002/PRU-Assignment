namespace GameplayScene.Ability.Effects
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;
    using Signals;
    using Zenject;

    public class DamageEffect : BaseEffect
    {
        public SignalBus SignalBus { get; }

        public DamageEffect(IAbilitySystem abilitySystem, SkillBlueprint.EffectRecord effectRecord, SignalBus signalBus) : base(abilitySystem, effectRecord)
        {
            this.SignalBus = signalBus;
        }

        public override string EffectID => "damage";

        public override void UpdatePerFrame(float deltaTime)
        {
            this.Entity.Health -= this.EffectRecord.Value;

            this.SignalBus.Fire(new TookDamageSignal
            {
                Entity = this.Entity,
                Damage = (long)this.EffectRecord.Value
            });

            base.UpdatePerFrame(deltaTime);
        }
    }
}