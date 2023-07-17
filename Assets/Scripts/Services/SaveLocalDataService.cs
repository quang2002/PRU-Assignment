namespace Services
{
    using GDK.LocalData;
    using GDK.Services.Unity;
    using Zenject;

    public class SaveLocalDataService : IInitializable
    {
        private SignalBus        SignalBus        { get; }
        private LocalDataManager LocalDataManager { get; }

        public SaveLocalDataService(SignalBus signalBus, LocalDataManager localDataManager)
        {
            this.SignalBus        = signalBus;
            this.LocalDataManager = localDataManager;
        }

        public void Initialize()
        {
            this.SignalBus.Subscribe<ApplicationPauseSignal>(this.SaveLocalData);
            this.SignalBus.Subscribe<ApplicationQuitSignal>(this.SaveLocalData);
        }

        private void SaveLocalData()
        {
            this.LocalDataManager.SaveAllLocalData();
        }
    }
}