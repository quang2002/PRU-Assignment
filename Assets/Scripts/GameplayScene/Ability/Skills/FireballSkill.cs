namespace GameplayScene.Ability.Skills
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;
    using UnityEngine;

    public class FireballSkill : BaseSkill
    {
        public ILogger Logger { get; }

        public FireballSkill(SkillBlueprint skillBlueprint,
                             ILogger        logger
        ) : base(skillBlueprint)
        {
            this.Logger = logger;
        }

        public override string SkillID => "fireball";

        public override void Perform()
        {
            this.Logger.Log("Cast fireball");
        }
    }
}