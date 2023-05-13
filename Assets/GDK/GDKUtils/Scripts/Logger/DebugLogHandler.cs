namespace GDK.GDKUtils.Scripts.Logger
{
    using System;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class DebugLogHandler : ILogHandler
    {
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            Debug.LogFormat(logType, LogOption.None, context, format, args);
        }

        public void LogException(Exception exception, Object context)
        {
            Debug.LogException(exception, context);
        }
    }
}