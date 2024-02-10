using System.IO;
using UnityEditor;
using UnityEngine;

namespace Cobilas.Unity.Editor.Utility {
    public static class UnityFolder {

        public static string RootFolder => Path.GetDirectoryName(Application.dataPath);

        [MenuItem("Tools/Unity folder/Utility folder")]
        [MenuItem("Assets/Create/Unity folder/Utility folder")]
        private static void InitUtilityFolder()
            => CriateFolder("Utility");

        [MenuItem("Tools/Unity folder/Resources folder")]
        [MenuItem("Assets/Create/Unity folder/Resources folder")]
        private static void InitResourcesFolder()
            => CriateFolder("Resources");

        [MenuItem("Tools/Unity folder/Editor folder")]
        [MenuItem("Assets/Create/Unity folder/Editor folder")]
        private static void InitEditorFolder()
            => CriateFolder("Editor");

        [MenuItem("Tools/Unity folder/Streaming assets folder")]
        [MenuItem("Assets/Create/Unity folder/Streaming assets folder")]
        private static void InitStreamingAssetsFolder()
            => CriateFolder("StreamingAssets");

        [MenuItem("Assets/Create/Unity folder/ScriptTDS folder")]
        private static void InitScriptTDSFolder()
            => CriateFolder("ScriptTDS");

        [MenuItem("Tools/Unity folder/Script folder")]
        [MenuItem("Assets/Create/Unity folder/Script folder")]
        private static void InitScriptFolder()
            => CriateFolder("Script");

        [MenuItem("Assets/Create/Unity folder/Prefabs folder")]
        private static void InitPrefabsFolder()
            => CriateFolder("Prefabs");

        [MenuItem("Assets/Create/Unity folder/Textures folder")]
        private static void InitTexturesFolder()
            => CriateFolder("Textures");

        [MenuItem("Tools/Unity folder/Dlls folder")]
        [MenuItem("Assets/Create/Unity folder/Dlls folder")]
        private static void InitDllsFolder()
            => CriateFolder("Dlls");

        [MenuItem("Tools/Unity folder/Auxiliary classes folder")]
        [MenuItem("Assets/Create/Unity folder/Auxiliary classes folder")]
        private static void InitClassesAuxiliaresFolder()
            => CriateFolder("Auxiliary classes");

        [MenuItem("Tools/Unity folder/Attributes folder")]
        [MenuItem("Assets/Create/Unity folder/Attributes folder")]
        private static void InitAtributosFolder()
            => CriateFolder("Attributes");

        [MenuItem("Tools/Unity folder/Interfaces folder")]
        [MenuItem("Assets/Create/Unity folder/Interfaces folder")]
        private static void InitInterfacesFolder()
            => CriateFolder("Interfaces");

        [MenuItem("Tools/Unity folder/Extensions folder")]
        [MenuItem("Assets/Create/Unity folder/Extensions folder")]
        private static void InitExtencoesFolder()
            => CriateFolder("Extensions");

        [MenuItem("Tools/Unity folder/Shaders folder")]
        [MenuItem("Assets/Create/Unity folder/Shaders folder")]
        private static void InitShadersFolder()
            => CriateFolder("Shaders");

        [MenuItem("Tools/Unity folder/Materiais folder")]
        [MenuItem("Assets/Create/Unity folder/Materiais folder")]
        private static void InitMateriaisFolder()
            => CriateFolder("Materiais");

        [MenuItem("Tools/Unity folder/Unity folders")]
        [MenuItem("Assets/Create/Unity folder/Unity folders")]
        private static void InitUnityFolders() {
            InitResourcesFolder();
            InitEditorFolder();
            InitStreamingAssetsFolder();
        }

        private static void CriateFolder(string nameFolder) {
            string assetfolderpath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (AssetDatabase.IsValidFolder(assetfolderpath))
                AssetDatabase.CreateFolder(assetfolderpath, nameFolder);
            else AssetDatabase.CreateFolder("Assets", nameFolder);
            AssetDatabase.Refresh();
        }
    }
}