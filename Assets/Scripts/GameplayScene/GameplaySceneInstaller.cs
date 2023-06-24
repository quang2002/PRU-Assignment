namespace GameplayScene
{
    using GameplayScene.Screens;
    using GDK;
    using GDK.UIManager.Scripts;

    public class GameplaySceneInstaller : GDKSceneContext
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            this.Container.Resolve<UIManager>().OpenScreen<GameplayScreen>();
        }
    }
}