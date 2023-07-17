namespace GameplayScene.Ability.Skills
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;
    using UnityEngine;

    public class DeadShearSkill : BaseSkill
    {
        public ILogger Logger { get; }

        public DeadShearSkill(SkillBlueprint skillBlueprint,
                              ILogger        logger
        ) : base(skillBlueprint)
        {
            this.Logger = logger;
        }

        public override string SkillID => "death-shear";

        public override void Perform()
        {
            this.Logger.Log("Cast dead shear skill");
        }
    }
}