namespace GameplayScene.Ability.System
{
    using global::System;
    using global::System.Collections.Generic;
    using Models.Blueprint;
    using Zenject;

    public abstract class BaseEffect
    {
        #region Inject

        private IAbilitySystem AbilitySystem { get; }

        protected BaseEffect(IAbilitySystem abilitySystem, SkillBlueprint.EffectRecord effectRecord)
        {
            this.EffectRecord  = effectRecord;
            this.AbilitySystem = abilitySystem;
        }

        #endregion

        public abstract string EffectID { get; }

        public IEntity Entity   { get; private set; }
        public float   Duration { get; private set; }

        public SkillBlueprint.EffectRecord EffectRecord { get; }

        public void ApplyEffect(IEntity entity)
        {
            this.Entity   = entity;
            this.Duration = this.EffectRecord.Duration;
            entity.Effects.Add(this);

            this.AbilitySystem.AddEffect(this);
        }

        public void RemoveEffect()
        {
            this.AbilitySystem.RemoveEffect(this);
            this.Entity.Effects.Remove(this);
            this.Duration = 0;
            this.Entity   = null;
        }

        public void UpdatePerFrame(float deltaTime)
        {
            this.Duration -= deltaTime;
            if (this.Duration <= 0) this.RemoveEffect();
        }
    }

    public class EffectFactory : IFactory<SkillBlueprint.EffectRecord, BaseEffect>
    {
        public  DiContainer              Container { get; }
        private Dictionary<string, Type> IdToType  { get; } = new();

        public EffectFactory(List<BaseEffect> effects, DiContainer container)
        {
            this.Container = container;
            foreach (var effect in effects)
            {
                this.IdToType.Add(effect.EffectID, effect.GetType());
            }
        }

        public BaseEffect Create(SkillBlueprint.EffectRecord param)
        {
            var type = this.IdToType[param.ID];
            return (BaseEffect)this.Container.Instantiate(type, new[] { param });
        }
    }
}