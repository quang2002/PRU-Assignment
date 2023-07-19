namespace GameplayScene.Ability.Skills
{
    using GameplayScene.Ability.System;
    using Models.Blueprint;
    using Models.DataControllers;
    using Zenject;

    public class FireballSkill : BaseSkill
    {
        public FireballSkill(SkillBlueprint          skillBlueprint,
                             SignalBus               signalBus,
                             InventoryDataController inventoryDataController
        ) : base(skillBlueprint, signalBus, inventoryDataController)
        {
        }

        public override string SkillID => "fireball";

        protected override void Perform()
        {
        }
    }
}