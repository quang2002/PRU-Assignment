namespace GDK.GDKUtils.Scripts
{
    using GDK.GDKUtils.Scripts.Logger;
    using Zenject;

    public static class LoggerUtils
    {
        public static void InstallDebugLogger(this DiContainer container)
        {
            LoggerInstaller<DebugLogHandler>.Install(container);
        }
    }
}