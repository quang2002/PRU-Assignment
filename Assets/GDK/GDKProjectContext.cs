namespace GDK
{
    using GDK.GDKUtils.Scripts;
    using GDK.LocalData.Scripts;
    using Zenject;

    public class GDKProjectContext : MonoInstaller<GDKProjectContext>
    {
        public override void InstallBindings()
        {
            this.Container.InstallDebugLogger();

            LocalDataInstaller.Install(this.Container);
        }
    }
}