namespace GameplayScene.MapGenerator.Editor
{
    using UnityEditor;
    using UnityEngine;

    [EditorWindowTitle(title = "Create Room Template")]
    public class CreateRoomTemplate : EditorWindow
    {
        private string RoomTemplateName { get; set; }

        private void OnGUI()
        {
            GUILayout.Label("Enter room template name:");
            this.RoomTemplateName = GUILayout.TextField(this.RoomTemplateName);

            if (GUILayout.Button("Create", GUILayout.Height(30)))
            {
                if (string.IsNullOrEmpty(this.RoomTemplateName))
                {
                    EditorUtility.DisplayDialog("Error", "Room template name cannot be empty", "OK");
                    return;
                }

                var gameObject = new GameObject(this.RoomTemplateName);

                gameObject.AddComponent<RoomTemplate>();

                var path = AssetDatabase.GenerateUniqueAssetPath($"Assets/Prefabs/RoomTemplates/{this.RoomTemplateName}.prefab");

                PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, path, InteractionMode.AutomatedAction);

                DestroyImmediate(gameObject);
            }
        }

        [MenuItem("PRU/Map Generator/Create Room Template")]
        private static void ShowWindow()
        {
            var window = CreateInstance<CreateRoomTemplate>();
            window.ShowAsDropDown(window.position, window.minSize);
        }
    }
}