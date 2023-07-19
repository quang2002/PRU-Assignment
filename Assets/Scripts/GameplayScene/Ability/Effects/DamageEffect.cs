namespace GameplayScene.Ability.Effects
{
    using GameplayScene.Ability.System;
    using Signals;
    using Zenject;

    public class DamageEffect : BaseEffect
    {
        public SignalBus SignalBus { get; }

        public float DamageInterval { get; private set; }

        public DamageEffect(EffectData effectData, SignalBus signalBus) : base(effectData)
        {
            this.SignalBus = signalBus;
        }

        public override void UpdatePerFrame(float deltaTime)
        {
            this.DamageInterval -= deltaTime;

            if (this.DamageInterval <= 0)
            {
                this.Entity.Health -= this.Data.Value;
                this.SignalBus.Fire(new TookDamageSignal
                {
                    Entity = this.Entity,
                    Damage = this.Data.Value
                });

                this.DamageInterval = 1f;
            }
        }
    }
}