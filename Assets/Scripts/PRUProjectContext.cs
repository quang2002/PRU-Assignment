using GDK;
using Services;
using Signals;
using Zenject;

public class PRUProjectContext : GDKProjectContext
{
    public override void InstallBindings()
    {
        base.InstallBindings();

        this.Container.DeclareSignal<EquippedSkillSignal>();
        this.Container.DeclareSignal<CastedSkillSignal>();
        this.Container.DeclareSignal<UpgradedStatSignal>();
        this.Container.DeclareSignal<TookDamageSignal>();

        this.Container.BindInterfacesTo<SaveLocalDataService>().AsCached().NonLazy();
    }
}