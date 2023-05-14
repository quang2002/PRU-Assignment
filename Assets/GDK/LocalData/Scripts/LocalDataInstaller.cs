namespace GDK.LocalData.Scripts
{
    using Zenject;

    public class LocalDataInstaller : Installer<LocalDataInstaller>
    {
        public override void InstallBindings()
        {
            this.Container
                .Bind<ILocalData>()
                .To(binder => binder.AllNonAbstractClasses())
                .AsCached();

            this.Container
                .ResolveAll<ILocalData>()
                .ForEach(data =>
                             this.Container
                                 .Bind(data.GetType())
                                 .FromInstance(data)
                                 .AsCached()
                );
            
            this.Container.Bind<LocalDataManager>().AsCached();
        }
    }
}