namespace GDK.AudioManager.Editor
{
    using GDK.AudioManager.Scripts;
    using GDK.GDKUtils.Editor;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerEditor : Editor<AudioManager>
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Add Audio", GUILayout.Height(32)))
                this.target.AddAudioData();
        }
    }
}