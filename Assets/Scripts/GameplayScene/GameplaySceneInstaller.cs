namespace GameplayScene
{
    using GameplayScene.Ability.System;
    using GameplayScene.Entity;
    using GameplayScene.Screens;
    using GDK;
    using GDK.UIManager;

    public class GameplaySceneInstaller : GDKSceneContext
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            AbilitySystemInstaller.Install(this.Container);

            this.Container
                .Bind<Player>()
                .FromComponentInHierarchy()
                .AsCached();

            this.Container
                .Bind<EnemyObjectPool>()
                .FromComponentInHierarchy()
                .AsCached()
                .NonLazy();

            this.Container
                .Bind<IEnemyBehaviour>()
                .To(binder => binder.AllNonAbstractClasses().DerivingFrom<IEnemyBehaviour>())
                .AsCached()
                .WhenInjectedInto<EnemyBehaviourProvider>();

            this.Container
                .Bind<EnemyBehaviourProvider>()
                .AsCached()
                .NonLazy();

            this.Container
                .BindInterfacesTo<EnemySpawner>()
                .AsCached()
                .NonLazy();

            this.Container.Resolve<UIManager>().OpenScreen<GameplayScreen>();
        }
    }
}