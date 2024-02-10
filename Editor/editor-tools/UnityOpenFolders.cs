using UnityEngine;
using UnityEditor;
using System.Diagnostics;

namespace Cobilas.Unity.Editor.Utility {
    public static class UnityOpenFolders
    {
        [MenuItem("Tools/Unity open folders/Assets path")]
        private static void AssetsPath()
            => OpenFolder(Application.dataPath);

        [MenuItem("Tools/Unity open folders/Project folder path")]
        private static void ProjectFolderPath()
            => OpenFolder(System.IO.Path.GetDirectoryName(Application.dataPath));

        [MenuItem("Tools/Unity open folders/Console log path")]
        private static void ConsoleLogPath()
            => OpenFolder(Application.consoleLogPath);

        [MenuItem("Tools/Unity open folders/Persistent path")]
        private static void PersistentDataPath()
            => OpenFolder(Application.persistentDataPath);

        [MenuItem("Tools/Unity open folders/Temporary cache path")]
        private static void TemporaryCachePath()
            => OpenFolder(Application.temporaryCachePath);

        [MenuItem("Tools/Unity open folders/Editor contents path")]
        private static void EditorContentsPath()
            => OpenFolder(EditorApplication.applicationContentsPath);

        private static void OpenFolder(string folderPath) {
#if PLATFORM_STANDALONE_WIN
            folderPath = folderPath.Replace('/', '\\');
#else
            folderPath = folderPath.Replace('\\', '/');
#endif
            MonoBehaviour.print(folderPath);
            Process.Start(folderPath).Dispose();
        }
    }
}