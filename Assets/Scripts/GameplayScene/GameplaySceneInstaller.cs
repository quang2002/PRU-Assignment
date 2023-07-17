namespace GameplayScene
{
    using GameplayScene.Ability.System;
    using GameplayScene.Screens;
    using GDK;
    using GDK.UIManager;

    public class GameplaySceneInstaller : GDKSceneContext
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            AbilitySystemInstaller.Install(this.Container);

            this.Container.Resolve<UIManager>().OpenScreen<GameplayScreen>();
        }
    }
}