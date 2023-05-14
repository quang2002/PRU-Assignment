namespace GDK.UIManager.Scripts
{
    using Zenject;

    public class UIManagerInstaller : Installer<UIManager, UIManagerInstaller>
    {
        public UIManagerInstaller(UIManager uiManager)
        {
            this.UIManager = uiManager;
        }

        public UIManager UIManager { get; }

        public override void InstallBindings()
        {
            this.Container.Bind<UIManager>().FromInstance(this.UIManager).AsCached();
        }
    }
}