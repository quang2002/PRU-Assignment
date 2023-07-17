namespace GameplayScene.Ability.System
{
    using Models.Blueprint;
    using Zenject;

    public class AbilitySystemInstaller : Installer<AbilitySystemInstaller>
    {
        public override void InstallBindings()
        {
            this.Container
                .Bind<BaseEffect>()
                .To(binder => binder.AllNonAbstractClasses().DerivingFrom<BaseEffect>())
                .AsCached()
                .WithArgumentsExplicit(new[]
                {
                    new TypeValuePair(typeof(SkillBlueprint.EffectRecord), null)
                })
                .WhenInjectedInto<EffectFactory>();

            this.Container
                .Bind<EffectFactory>()
                .AsCached()
                .NonLazy();

            this.Container
                .Bind<BaseSkill>()
                .To(binder => binder.AllNonAbstractClasses().DerivingFrom<BaseSkill>())
                .AsCached()
                .WhenInjectedInto<AbilitySystem>();

            this.Container
                .BindInterfacesTo<AbilitySystem>()
                .AsCached()
                .NonLazy();
        }
    }
}