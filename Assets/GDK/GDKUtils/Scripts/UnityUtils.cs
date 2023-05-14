namespace GDK.GDKUtils.Scripts
{
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;

    public static class UnityUtils
    {
        public static void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}