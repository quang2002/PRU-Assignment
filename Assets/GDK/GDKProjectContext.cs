namespace GDK
{
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

            LocalDataInstaller.Install(this.Container);

            UnityServiceInstaller.Install(this.Container);
        }
    }
}