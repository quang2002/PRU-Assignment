namespace GameplayScene.MapGenerator.Editor
{
    using GDK.GDKUtils.Editor;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorInspector : Editor<MapGenerator>
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Generate", GUILayout.Height(30)))
            {
                this.target.Generate();
            }

            EditorGUILayout.Separator();

            base.OnInspectorGUI();
        }
    }
}