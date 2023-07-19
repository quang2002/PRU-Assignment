namespace GameplayScene.Ability.Skills
{
    using Common;
    using GameplayScene.Ability.Effects;
    using GameplayScene.Ability.System;
    using GameplayScene.Entity;
    using GDK.GDKUtils;
    using Models.Blueprint;
    using Models.DataControllers;
    using Services;
    using UnityEngine;
    using Zenject;

    public class DeadShearSkill : BaseSkill
    {
        public VFXService              VFXService              { get; }
        public Player                  Player                  { get; }
        public MainLocalDataController MainLocalDataController { get; }

        public DeadShearSkill(SkillBlueprint          skillBlueprint,
                              SignalBus               signalBus,
                              VFXService              vfxService,
                              Player                  player,
                              MainLocalDataController mainLocalDataController
        ) : base(skillBlueprint, signalBus)
        {
            this.VFXService              = vfxService;
            this.Player                  = player;
            this.MainLocalDataController = mainLocalDataController;
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
                    Value      = this.MainLocalDataController.GetStatValue(StatType.Attack) * 10f
                }, enemy);
            }
        }
    }
}