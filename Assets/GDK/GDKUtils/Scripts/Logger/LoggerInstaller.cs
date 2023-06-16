namespace GDK.GDKUtils.Scripts.Logger
{
    using UnityEngine;
    using Zenject;

    public class LoggerInstaller<T> : Installer<T, LoggerInstaller<T>> where T : ILogHandler
    {
        public LoggerInstaller(T logHandler)
        {
            this.LogHandler = logHandler;
        }

        public T LogHandler { get; }

        public override void InstallBindings()
        {
            this.Container
                .Bind<ILogger>()
                .FromInstance(new Logger(this.LogHandler))
                .AsCached()
                .NonLazy();
        }

        public static void Install(DiContainer container)
        {
            Install(container, container.Instantiate<T>());
        }
    }
}