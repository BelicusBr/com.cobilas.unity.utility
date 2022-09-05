using System;

namespace Cobilas.Unity.Utility {
    [Obsolete("Use Cobilas.Unity.Utility.UnityPath class")]
    public static class CobilasPaths {

        public static string AssetsPath => UnityPath.AssetsPath;
        public static string PersistentDataPath => UnityPath.PersistentDataPath;
        public static string StreamingAssetsPath => UnityPath.StreamingAssetsPath;
        public static string ResourcesPath {
#if UNITY_EDITOR
            get => Combine(AssetsPath, "Resources");
#else
            get => "Resources";
#endif
        }
#if UNITY_EDITOR
        /// <summary>ProjectFolderPath in editor only</summary>
        public static string ProjectFolderPath => UnityPath.ProjectFolderPath;
#endif

        public static string GetExtension(string path)
            => UnityPath.GetExtension(path);

        public static string GetFileNameWithoutExtension(string path)
            => UnityPath.GetFileNameWithoutExtension(path);

        public static string GetFileName(string path)
            => UnityPath.GetFileName(path);

        public static string GetDirectoryName(string path)
            => UnityPath.GetDirectoryName(path);

        public static string Combine(string path1, string path2, string path3, string path4)
            => Combine(new string[] { path1, path2, path3, path4 });

        public static string Combine(string path1, string path2, string path3)
            => Combine(new string[] { path1, path2, path3 });

        public static string Combine(string path1, string path2)
            => Combine(new string[] { path1, path2 });

        public static string Combine(params string[] paths)
            => UnityPath.Combine(paths);
    }
}
