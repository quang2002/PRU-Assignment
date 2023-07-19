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

    public class HolyBladeSkill : BaseSkill
    {
        public Player     Player     { get; }
        public VFXService VFXService { get; }

        public HolyBladeSkill(SkillBlueprint          skillBlueprint,
                              SignalBus               signalBus,
                              InventoryDataController inventoryDataController,
                              Player                  player,
                              VFXService              vfxService
        ) : base(skillBlueprint, signalBus, inventoryDataController)
        {
            this.Player     = player;
            this.VFXService = vfxService;
        }

        public override string SkillID => "holy-blade";

        protected override void Perform()
        {
            var position = this.Player.transform.position;
            _ = this.VFXService.SpawnVFX("vfx-holy-blade", position + new Vector3(  5f, -0.7f), Quaternion.Euler(0, 90, 0), Vector3.one * 0.7f);

            foreach (var enemy in this.Player.Enemies)
            {
                this.AbilitySystem.ApplyEffect(new BaseEffect.EffectData
                {
                    EffectType = typeof(DamageEffect),
                    Value      = 5f * this.SkillData.Level
                }, enemy);
            }
        }
    }
}