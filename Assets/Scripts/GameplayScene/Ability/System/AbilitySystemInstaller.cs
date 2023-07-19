namespace GameplayScene.Ability.System
{
    using Zenject;

    public class AbilitySystemInstaller : Installer<AbilitySystemInstaller>
    {
        public override void InstallBindings()
        {
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