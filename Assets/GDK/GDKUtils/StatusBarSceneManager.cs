namespace GDK.GDKUtils
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using ModestTree;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UIElements;

    [InitializeOnLoad]
    public static class StatusBarSceneManager
    {
        private static ScriptableObject statusBar;
        private static string[]         scenePaths;
        private static string[]         sceneNames;

        static StatusBarSceneManager()
        {
            EditorApplication.delayCall += () =>
            {
                EditorApplication.update -= Update;
                EditorApplication.update += Update;
            };
        }

        private static void Update()
        {
            if (statusBar == null)
            {
                var editorAssembly = typeof(Editor).Assembly;

                var statusBars = Resources.FindObjectsOfTypeAll(editorAssembly.GetType("UnityEditor.AppStatusBar"));
                statusBar = statusBars.Length > 0 ? (ScriptableObject)statusBars[0] : null;
                if (statusBar != null)
                {
                    if (statusBar.GetType()
                                 .GetProperty("visualTree", BindingFlags.Instance | BindingFlags.NonPublic)
                                 ?.GetValue(statusBar) is not VisualElement visualTree) return;

                    visualTree.style.display       = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                    visualTree.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);

                    var statusBarContainer = visualTree.Q<IMGUIContainer>();
                    statusBarContainer.style.position = new StyleEnum<Position>(Position.Relative);

                    visualTree.Insert(0, new IMGUIContainer
                    {
                        onGUIHandler = OnGUI,
                        style =
                        {
                            paddingRight = new StyleLength(25)
                        }
                    });
                }
            }

            var localScenePaths = new List<string>();
            var localSceneNames = new List<string>();

            var folderName   = Application.dataPath + "/Scenes";
            var dirInfo      = new DirectoryInfo(folderName);
            var allFileInfos = dirInfo.GetFiles("*.unity", SearchOption.AllDirectories);
            foreach (var fileInfo in allFileInfos)
            {
                var fullPath  = fileInfo.FullName.Replace(@"\", "/");
                var scenePath = "Assets" + fullPath.Replace(Application.dataPath, "");

                localScenePaths.Add(scenePath);
                localSceneNames.Add(Path.GetFileNameWithoutExtension(scenePath));
            }

            scenePaths = localScenePaths.ToArray();
            sceneNames = localSceneNames.ToArray();
        }

        private static void OnGUI()
        {
            using (new EditorGUI.DisabledScope(Application.isPlaying))
            {
                var sceneName  = SceneManager.GetActiveScene().name;
                var sceneIndex = sceneNames.IndexOf(sceneName);

                var newSceneIndex = EditorGUILayout.Popup(sceneIndex, sceneNames, GUILayout.Width(200.0f));

                if (newSceneIndex == sceneIndex) return;

                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    EditorSceneManager.OpenScene(scenePaths[newSceneIndex], OpenSceneMode.Single);
            }
        }
    }
}