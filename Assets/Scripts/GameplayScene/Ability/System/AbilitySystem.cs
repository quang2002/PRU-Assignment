namespace GameplayScene.Ability.System
{
    using global::System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class AbilitySystem : ITickable, IAbilitySystem
    {
        private HashSet<BaseEffect> Effects { get; } = new();

        private Dictionary<string, BaseSkill> Skills { get; } = new();

        public AbilitySystem(List<BaseSkill> skills)
        {
            foreach (var skill in skills)
            {
                this.Skills.Add(skill.SkillID, skill);
            }
        }

        public void AddEffect(BaseEffect effect)
        {
            this.Effects.Add(effect);
        }

        public void RemoveEffect(BaseEffect effect)
        {
            this.Effects.Remove(effect);
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
            foreach (var effect in this.Effects)
            {
                effect.UpdatePerFrame(Time.deltaTime);
            }

            foreach (var (_, skill) in this.Skills)
            {
                skill.UpdateCooldown(Time.deltaTime);
            }
        }
    }
}