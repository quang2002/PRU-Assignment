namespace GameplayScene.Ability.Effects
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;
    using Signals;
    using Zenject;

    public class DamageEffect : BaseEffect
    {
        public SignalBus SignalBus { get; }

        public DamageEffect(EffectData effectData, SignalBus signalBus) : base(effectData)
        {
            this.SignalBus = signalBus;
        }

        public override void UpdatePerFrame(float deltaTime)
        {
            this.Entity.Health -= this.Data.Value;

            this.SignalBus.Fire(new TookDamageSignal
            {
                Entity = this.Entity,
                Damage = this.Data.Value
            });
        }
    }
}