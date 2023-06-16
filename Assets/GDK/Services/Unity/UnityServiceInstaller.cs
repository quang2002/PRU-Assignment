namespace GDK.Services.Unity
{
    using Zenject;

    public class UnityServiceInstaller : Installer<UnityServiceInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.DeclareSignal<ApplicationFocusSignal>();
            this.Container.DeclareSignal<ApplicationPauseSignal>();
            this.Container.DeclareSignal<ApplicationQuitSignal>();

            this.Container
                .Bind<UnityService>()
                .FromNewComponentOnNewGameObject()
                .AsCached()
                .NonLazy();
        }
    }
}