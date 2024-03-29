namespace GameplayScene.Ability.Skills
{
    using GameplayScene.Ability.Effects;
    using GameplayScene.Ability.System;
    using GameplayScene.Entity;
    using Models.Blueprint;
    using Models.DataControllers;
    using Services;
    using UnityEngine;
    using Zenject;

    public class DeadShearSkill : BaseSkill
    {
        public VFXService VFXService { get; }
        public Player     Player     { get; }

        public DeadShearSkill(SkillBlueprint          skillBlueprint,
                              SignalBus               signalBus,
                              InventoryDataController inventoryDataController,
                              VFXService              vfxService,
                              Player                  player
        ) : base(skillBlueprint, signalBus, inventoryDataController)
        {
            this.VFXService = vfxService;
            this.Player     = player;
        }

        public override string SkillID => "death-shear";

        protected override void Perform()
        {
            var position = this.Player.transform.position;
            _ = this.VFXService.SpawnVFX("vfx-death-shear", position, Quaternion.Euler(0, 90, 0));

            foreach (var enemy in this.Player.Enemies)
            {
                this.AbilitySystem.ApplyEffect(new BaseEffect.EffectData
                {
                    EffectType = typeof(DamageEffect),
                    Value      = this.SkillData.Level,
                    Duration   = 10f
                }, enemy);
            }
        }
    }
}