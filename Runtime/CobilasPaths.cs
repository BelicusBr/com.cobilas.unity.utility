using System.IO;

namespace Cobilas.Unity.Utility {
    public static class CobilasPaths {

        public static string AssetsPath => UnityEngine.Application.dataPath;
        public static string PersistentDataPath => UnityEngine.Application.persistentDataPath;
        public static string StreamingAssetsPath => UnityEngine.Application.streamingAssetsPath;
        public static string ResourcesPath {
#if UNITY_EDITOR
            get => Combine(AssetsPath, "Resources");
#else
            get => "Resources";
#endif
        }
#if UNITY_EDITOR
        public static string ProjectFolderPath => GetDirectoryName(UnityEngine.Application.dataPath);
#endif

        public static string GetExtension(string path)
            => Path.GetExtension(path);

        public static string GetFileNameWithoutExtension(string path)
            => ReadjustPath(Path.GetFileNameWithoutExtension(path));

        public static string GetFileName(string path)
            => ReadjustPath(Path.GetFileName(path));

        public static string GetDirectoryName(string path)
            => ReadjustPath(Path.GetDirectoryName(path));

        public static string Combine(string path1, string path2, string path3, string path4)
            => Combine(new string[] { path1, path2, path3, path4 });

        public static string Combine(string path1, string path2, string path3)
            => Combine(new string[] { path1, path2, path3 });

        public static string Combine(string path1, string path2)
            => Combine(new string[] { path1, path2 });

        public static string Combine(params string[] paths)
            => ReadjustPath(Path.Combine(paths));

        public static string ReadjustPath(string path)
            => path.Replace('\\', '/');
    }
}
