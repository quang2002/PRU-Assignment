namespace GameplayScene.Ability.System
{
    using global::System.Collections.Generic;
    using global::System.Linq;
    using UnityEngine;
    using Zenject;

    public class AbilitySystem : ITickable, IAbilitySystem
    {
        private HashSet<BaseEffect>           Effects { get; } = new();
        private Dictionary<string, BaseSkill> Skills  { get; } = new();

        public DiContainer Container { get; }

        public AbilitySystem(List<BaseSkill> skills, DiContainer container)
        {
            this.Container = container;

            foreach (var skill in skills)
            {
                skill.AbilitySystem = this;
                this.Skills.Add(skill.SkillID, skill);
            }
        }

        public void ApplyEffect(BaseEffect.EffectData effectData, IEntity entity)
        {
            var type   = effectData.EffectType;
            var effect = (BaseEffect)this.Container.Instantiate(type, new[] { effectData });

            this.Effects.Add(effect);
            effect.ApplyEffect(entity);
        }

        public void RemoveEffect(BaseEffect effect)
        {
            this.Effects.Remove(effect);
            effect.RemoveEffect();
        }

        public BaseSkill GetSkill(string skillID)
        {
            return this.Skills[skillID];
        }

        public IEnumerable<BaseSkill> GetAllSkills()
        {
            return this.Skills.Values;
        }

        public void Tick()
        {
            foreach (var effect in this.Effects.ToArray())
            {
                effect.UpdatePerFrame(Time.deltaTime);
                effect.Duration -= Time.deltaTime;
                if (effect.Duration <= 0) this.RemoveEffect(effect);
            }

            foreach (var (_, skill) in this.Skills)
            {
                skill.UpdateCooldown(Time.deltaTime);
            }
        }
    }
}