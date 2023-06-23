namespace GDK
{
    using GDK.AssetsManager.Scripts;
    using GDK.UIManager.Scripts;
    using UnityEngine;
    using Zenject;

    public class GDKSceneContext : MonoInstaller<GDKSceneContext>
    {
        [field: SerializeField]
        private UIManager.Scripts.UIManager UIManager { get; set; }

        public override void InstallBindings()
        {
            this.Container.Bind<IAssetsManager>().To<AssetsManager.Scripts.AssetsManager>().AsCached().NonLazy();

            UIManagerInstaller.Install(this.Container, this.UIManager);
        }
    }
}