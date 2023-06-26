namespace GDK
{

    using GDK.AssetsManager.Scripts;
    using GDK.BlueprintManager.Scripts;
    using GDK.GDKUtils.Scripts;
    using GDK.LocalData.Scripts;
    using GDK.Services.Unity;
    using Zenject;

    public class GDKProjectContext : MonoInstaller<GDKProjectContext>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(this.Container);

            this.Container.InstallDebugLogger();

            this.Container.Bind<IAssetsManager>().To<AssetsManager.Scripts.AssetsManager>().AsCached().NonLazy();

            BlueprintInstaller.Install(this.Container);

            LocalDataInstaller.Install(this.Container);

            UnityServiceInstaller.Install(this.Container);
        }
    }

}