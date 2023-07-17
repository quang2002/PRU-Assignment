namespace GameplayScene.Ability.System
{
    using Models.Blueprint;

    public abstract class BaseSkill
    {
        public abstract string SkillID { get; }
        public abstract void   Perform();

        public SkillBlueprint.SkillRecord SkillRecord     { get; private set; }
        public float                      Cooldown        { get; private set; }
        public float                      CooldownPercent => this.Cooldown / this.SkillRecord.Cooldown;

        protected BaseSkill(SkillBlueprint skillBlueprint)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.SkillRecord = skillBlueprint[this.SkillID];
        }

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
        }
    }
}