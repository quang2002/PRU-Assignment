namespace GDK.GDKUtils.Scripts
{
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public static class UnityUtils
    {
        public static void QuitApplication(int exitCode = 0)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit(exitCode);
        }

        public static void QuitApplication(this object _, int exitCode = 0)
        {
            QuitApplication(exitCode);
        }
    }
}