namespace GameplayScene.Ability.System
{
    using Models.Blueprint;
    using Signals;
    using Zenject;

    public abstract class BaseSkill
    {
        public abstract string                     SkillID         { get; }
        public          float                      Cooldown        { get; private set; }
        public          float                      CooldownPercent => this.Cooldown / this.SkillRecord.Cooldown;
        public          IAbilitySystem             AbilitySystem   { get; set; }
        public          SkillBlueprint.SkillRecord SkillRecord     { get; private set; }

        #region Inject

        protected SignalBus SignalBus { get; }

        protected BaseSkill(SkillBlueprint skillBlueprint,
                            SignalBus      signalBus)
        {
            this.SignalBus = signalBus;
            // ReSharper disable once VirtualMemberCallInConstructor
            this.SkillRecord = skillBlueprint[this.SkillID];
        }

        #endregion

        public void ResetCooldown()
        {
            this.Cooldown = this.SkillRecord.Cooldown;
        }

        public void UpdateCooldown(float deltaTime)
        {
            this.Cooldown -= deltaTime;
        }

        public void SetCooldown(float cooldown)
        {
            this.Cooldown = cooldown;
        }

        public bool IsReady()
        {
            return this.Cooldown <= 0;
        }

        public void Cast()
        {
            if (!this.IsReady())
            {
                return;
            }

            this.Perform();
            this.ResetCooldown();

            this.SignalBus.Fire(new CastedSkillSignal
            {
                Skill = this
            });
        }

        protected abstract void Perform();
    }
}