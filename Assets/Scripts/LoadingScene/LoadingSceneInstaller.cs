namespace LoadingScene
{
    using GDK;
    using GDK.UIManager;
    using LoadingScene.Screens;

    public class LoadingSceneInstaller : GDKSceneContext
    {
        public override void InstallBindings()
        {
            base.InstallBindings();

            this.Container.Resolve<UIManager>().OpenScreen<LoadingScreen>();
        }
    }
}