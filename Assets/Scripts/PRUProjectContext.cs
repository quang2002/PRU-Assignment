using GDK;
using Services;

public class PRUProjectContext : GDKProjectContext
{
    public override void InstallBindings()
    {
        base.InstallBindings();

        this.Container.BindInterfacesTo<SaveLocalDataService>().AsCached().NonLazy();
    }
}