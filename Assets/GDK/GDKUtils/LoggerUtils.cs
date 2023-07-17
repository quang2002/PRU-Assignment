namespace GDK.GDKUtils
{
    using GDK.GDKUtils.Logger;
    using Zenject;

    public static class LoggerUtils
    {
        public static void InstallDebugLogger(this DiContainer container)
        {
            LoggerInstaller<DebugLogHandler>.Install(container);
        }
    }
}