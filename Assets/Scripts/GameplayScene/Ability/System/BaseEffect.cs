namespace GameplayScene.Ability.System
{
    using global::System;

    public abstract class BaseEffect
    {
        public class EffectData
        {
            public Type    EffectType { get; init; }
            public float   Duration   { get; init; }
            public dynamic Value      { get; init; }
        }

        protected BaseEffect(EffectData effectData)
        {
            this.Data = effectData;
        }

        public IEntity    Entity   { get; private set; }
        public float      Duration { get; set; }
        public EffectData Data     { get; }

        public void ApplyEffect(IEntity entity)
        {
            this.Entity   = entity;
            this.Duration = this.Data.Duration;
            entity.Effects.Add(this);
        }

        public void RemoveEffect()
        {
            this.Entity.Effects.Remove(this);
            this.Duration = 0;
            this.Entity   = null;
        }

        public abstract void UpdatePerFrame(float deltaTime);
    }
}