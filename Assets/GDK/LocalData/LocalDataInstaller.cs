namespace GDK.LocalData
{
    using Zenject;

    public class LocalDataInstaller : Installer<LocalDataInstaller>
    {
        public override void InstallBindings()
        {
            this.Container
                .Bind<ILocalData>()
                .To(binder => binder.AllNonAbstractClasses())
                .AsCached()
                .OnInstantiated((_, obj) =>
                {
                    this.Container
                        .Bind(obj.GetType())
                        .FromInstance(obj)
                        .AsCached()
                        .WhenInjectedInto<ILocalDataController>();
                })
                .WhenInjectedInto<LocalDataManager>()
                .NonLazy();

            this.Container
                .Bind<LocalDataManager>()
                .AsCached()
                .NonLazy();

            this.Container
                .Bind(binder => binder.AllNonAbstractClasses().DerivingFrom<ILocalDataController>())
                .AsCached();
        }
    }
}