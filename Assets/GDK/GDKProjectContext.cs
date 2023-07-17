namespace GDK
{
    using GDK.AssetsManager;
    using GDK.BlueprintManager;
    using GDK.GDKUtils;
    using GDK.LocalData;
    using GDK.Services.Unity;
    using Zenject;

    public class GDKProjectContext : MonoInstaller<GDKProjectContext>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(this.Container);

            this.Container.InstallDebugLogger();

            this.Container.Bind<IAssetsManager>().To<AssetsManager.AssetsManager>().AsCached().NonLazy();

            BlueprintInstaller.Install(this.Container);

            LocalDataInstaller.Install(this.Container);

            UnityServiceInstaller.Install(this.Container);
        }
    }

}