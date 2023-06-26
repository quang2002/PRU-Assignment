namespace GDK
{

    using GDK.UIManager.Scripts;
    using UnityEngine;
    using Zenject;

    public class GDKSceneContext : MonoInstaller<GDKSceneContext>
    {
        [field: SerializeField]
        private UIManager.Scripts.UIManager UIManager { get; set; }

        public override void InstallBindings()
        {
            UIManagerInstaller.Install(this.Container, this.UIManager);
        }
    }

}