namespace GameplayScene.Ability.Skills
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;
    using Zenject;

    public class FireballSkill : BaseSkill
    {
        public FireballSkill(SkillBlueprint skillBlueprint,
                             SignalBus      signalBus
        ) : base(skillBlueprint, signalBus)
        {
        }

        public override string SkillID => "fireball";

        protected override void Perform()
        {
        }
    }
}