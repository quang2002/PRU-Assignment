namespace GameplayScene.Ability.Skills
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;
    using Zenject;

    public class DeadShearSkill : BaseSkill
    {
        public DeadShearSkill(SkillBlueprint skillBlueprint,
                              SignalBus      signalBus
        ) : base(skillBlueprint, signalBus)
        {
        }

        public override string SkillID => "death-shear";

        public override void Perform()
        {
        }
    }
}