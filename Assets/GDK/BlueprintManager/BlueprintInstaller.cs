namespace GDK.BlueprintManager
{
    using Zenject;

    public class BlueprintInstaller : Installer<BlueprintInstaller>
    {
        public override void InstallBindings()
        {
            this.Container
                .Bind(binder => binder
                                .AllNonAbstractClasses()
                                .DerivingFrom<IBlueprint>()
                ).AsCached()
                .OnInstantiated(
                    (_, obj) => (obj as IBlueprint)?.LoadBlueprint()
                ).NonLazy();
        }
    }

}