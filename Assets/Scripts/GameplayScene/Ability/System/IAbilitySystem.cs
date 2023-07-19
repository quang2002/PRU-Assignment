namespace GameplayScene.Ability.System
{
    using global::System.Collections.Generic;
    using Models.Blueprint;

    public interface IAbilitySystem
    {
        public void ApplyEffect(BaseEffect.EffectData effectData, IEntity entity);
        public void RemoveEffect(BaseEffect           effect);

        public BaseSkill              GetSkill(string skillID);
        public IEnumerable<BaseSkill> GetAllSkills();
    }
}