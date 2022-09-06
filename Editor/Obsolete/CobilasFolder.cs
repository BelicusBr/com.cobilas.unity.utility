using System.IO;
using UnityEditor;
using UnityEngine;

namespace Cobilas.Unity.Editor.Utility {
    [System.Obsolete("Use Cobilas.Unity.Editor.Utility.UnityFolder class")]
    public static class CobilasFolder {

        public static string RootFolder => UnityFolder.RootFolder;
#if NO_CODE
        [MenuItem("Tools/Cobilas folder/Utility folder")]
        [MenuItem("Assets/Create/Cobilas folder/Utility folder")]
        private static void InitUtilityFolder()
            => CriateFolder("Utility");

        [MenuItem("Tools/Cobilas folder/Resources folder")]
        [MenuItem("Assets/Create/Cobilas folder/Resources folder")]
        private static void InitResourcesFolder()
            => CriateFolder("Resources");

        [MenuItem("Tools/Cobilas folder/Editor folder")]
        [MenuItem("Assets/Create/Cobilas folder/Editor folder")]
        private static void InitEditorFolder()
            => CriateFolder("Editor");

        [MenuItem("Tools/Cobilas folder/Streaming assets folder")]
        [MenuItem("Assets/Create/Cobilas folder/Streaming assets folder")]
        private static void InitStreamingAssetsFolder()
            => CriateFolder("StreamingAssets");

        [MenuItem("Assets/Create/Cobilas folder/ScriptTDS folder")]
        private static void InitScriptTDSFolder()
            => CriateFolder("ScriptTDS");

        [MenuItem("Tools/Cobilas folder/Script folder")]
        [MenuItem("Assets/Create/Cobilas folder/Script folder")]
        private static void InitScriptFolder()
            => CriateFolder("Script");

        [MenuItem("Assets/Create/Cobilas folder/Prefabs folder")]
        private static void InitPrefabsFolder()
            => CriateFolder("Prefabs");

        [MenuItem("Assets/Create/Cobilas folder/Textures folder")]
        private static void InitTexturesFolder()
            => CriateFolder("Textures");

        [MenuItem("Tools/Cobilas folder/Dlls folder")]
        [MenuItem("Assets/Create/Cobilas folder/Dlls folder")]
        private static void InitDllsFolder()
            => CriateFolder("Dlls");

        [MenuItem("Tools/Cobilas folder/Auxiliary classes folder")]
        [MenuItem("Assets/Create/Cobilas folder/Auxiliary classes folder")]
        private static void InitClassesAuxiliaresFolder()
            => CriateFolder("Auxiliary classes");

        [MenuItem("Tools/Cobilas folder/Attributes folder")]
        [MenuItem("Assets/Create/Cobilas folder/Attributes folder")]
        private static void InitAtributosFolder()
            => CriateFolder("Attributes");

        [MenuItem("Tools/Cobilas folder/Interfaces folder")]
        [MenuItem("Assets/Create/Cobilas folder/Interfaces folder")]
        private static void InitInterfacesFolder()
            => CriateFolder("Interfaces");

        [MenuItem("Tools/Cobilas folder/Extensions folder")]
        [MenuItem("Assets/Create/Cobilas folder/Extensions folder")]
        private static void InitExtencoesFolder()
            => CriateFolder("Extensions");

        [MenuItem("Tools/Cobilas folder/Shaders folder")]
        [MenuItem("Assets/Create/Cobilas folder/Shaders folder")]
        private static void InitShadersFolder()
            => CriateFolder("Shaders");

        [MenuItem("Tools/Cobilas folder/Materiais folder")]
        [MenuItem("Assets/Create/Cobilas folder/Materiais folder")]
        private static void InitMateriaisFolder()
            => CriateFolder("Materiais");

        [MenuItem("Tools/Cobilas folder/Unity folders")]
        [MenuItem("Assets/Create/Cobilas folder/Unity folders")]
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
#endif
    }
}