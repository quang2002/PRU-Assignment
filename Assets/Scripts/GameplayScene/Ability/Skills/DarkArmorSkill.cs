namespace GameplayScene.Ability.Skills
{
    using GameplayScene.Ability.Effects;
    using GameplayScene.Ability.System;
    using GameplayScene.Entity;
    using global::System.Threading.Tasks;
    using Models.Blueprint;
    using Models.DataControllers;
    using Services;
    using UnityEngine;
    using Zenject;

    public class DarkArmorSkill : BaseSkill
    {
        public Player Player { get; }

        public DarkArmorSkill(SkillBlueprint          skillBlueprint,
                              SignalBus               signalBus,
                              InventoryDataController inventoryDataController,
                              Player                  player
        ) : base(skillBlueprint, signalBus, inventoryDataController)
        {
            this.Player = player;
        }

        public override string SkillID => "dark-armor";

        protected override void Perform()
        {
            this.Player.Invisible = true;
            Task.Delay((int)(3000 + this.SkillData.Level * 100)).ContinueWith(_ =>
            {
                this.Player.Invisible = false;
            });
        }
    }
}